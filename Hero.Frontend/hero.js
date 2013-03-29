//undefined is a parameter that nothing is passed into
//to protect it from being overwritten by another library
//in the global namespace.
(function (hero, request, undefined) {
    //private instance data
    var heroOptions = {};
    var instance = {};
    var currentUser = {};

    heroOptions.endpoint = "http://localhost/Abilities/";
    instance.options = heroOptions;

    //private functions
    var extend = function (optionsToMerge) {
        for (var i in optionsToMerge) {
            if (heroOptions.hasOwnProperty(i)) {
                heroOptions[i] = optionsToMerge[i];
            }
        }
    };

    var deserializeAbilityList = function (abilityObjList) {
        var retArray = [];
        for (var index in abilityObjList) {
            retArray.push(deserializeAbility(abilityObjList[index]));
        }

        return retArray;
    };

    var deserializeAbility = function (abilityObj) {
        var ability = hero.Ability(abilityObj.Name, []);

        for (var childIndex in abilityObj.Children) {
            ability.children.push(new hero.Ability(abilityObj.Children[childIndex].Name, []));
        }

        return ability;
    };

    var deserializeRoleList = function (roleObjList) {
        var retArray = [];
        for (var index in roleObjList) {
            retArray.push(deserializeRole(roleObjList[index]));
        }

        return retArray;
    };

    var deserializeRole = function (roleObj) {
        return hero.Role(roleObj.Name);
    };

    /**
      * Returns a list of abilities for the current user.
      */
    var intializeCurrentUser = function () {
        hero.getCurrentUser(
            {
                then: function (userName) {
                    if (!userName) {
                        //create a fake user since this person is not authenticated
                        currentUser = hero.User("UnAuthenticated");
                        return;
                    }

                    currentUser = hero.User(userName);

                    //now that we have the current user, populate the abilitites.
                    hero.getAbilititesForUser({
                        user: currentUser,
                        then: function (abilities) {
                            currentUser.abilitites = abilities;
                        },
                        fail: function (err, msg) {
                            console.log(msg);
                        },
                        async: false
                    });
                },
                fail: function (err, msg) { console.log(msg); },
                async: false
            });
    };

    //Public Model Types
    hero.User = function (name, abilitites) {
        return {
            userName: name,
            abilitites: abilitites,
            isAuthorized: function (ability) {
                if (!ability || !ability.abilityName) {
                    throw "You must provide an ability to the isAuthorized function.";
                }

                for (var currAbilityIndex in this.abilitites) {
                    if (this.abilitites[currAbilityIndex].abilityName === ability.abilityName) {
                        return true;
                    }
                }

                return false;
            }
        };
    };

    hero.Role = function (name) {
        return { roleName: name };
    };

    hero.Ability = function (name, children) {
        return { abilityName: name, children: children };
    };

    //public API (configuration)
    hero.configure = function (options) {
        extend(options);
        intializeCurrentUser();
        return this;
    };

    /**
      * The registration can take a true object or a function as the second paramter
      * If only an object and no function are passed, it will register itself with every function off the object
      */
    hero.registerAbility = function (ability, target, fn) {
        if (!ability || !ability.abilityName)
            throw "You must pass a Hero.Ability as the first parameter for registration.";
        if (!target || !(target instanceof Object))
            throw "You must pass an object as the target.";
        if (!fn || !(fn instanceof Function)) {
            //if they dont' pass a function assume they want every function on that object registerd
            for (var potentialFunctionIndex in target) {
                if (target[potentialFunctionIndex] instanceof Function) {
                    hero.registerAbility(ability, target, target[potentialFunctionIndex]);
                }
            }
        }

        //if we are running registration the second time
        //you need to use the original function.
        if (fn.trueName) {
            fn = fn.originalFunction;
        }

        target[fn.name] = function () {
            var that = this;
            var thatArguments = arguments;

            var isAuthorized = currentUser.isAuthorized(ability);

            if (isAuthorized) {
                return fn.apply(that, thatArguments);
            } else {
                return "UserNotAuthorized";
            }
        };

        //setup the option of re-registering
        target[fn.name].trueName = fn.name;
        target[fn.name].originalFunction = fn;

        return this;
    };

    //public API (actions)

    /* authorizeCurrentUser
     * options.ability: Ability object
     * options.then: callback to perform action when the request completes successfully.  Parameters are (string userName)
     * options.fail: callback to perform an action on a request failure.  Parameters are (error, message)
     * options.always (optional): callback to perform when the request is over (failure or success). Parameters are (response)
     */
    hero.getCurrentUser = function (options) {
        if (!options.then)
            throw "You must provide a then callback to the options list.";
        if (!options.fail)
            throw "You must provide a fail callback to the options list.";

        request.get(instance.options.endpoint + "GetCurrentUser/")
            .then(function (resp) {
                //resp will be a boolean.
                options.then(resp);
            })
            .fail(function (err, msg) {
                options.then(err, msg);
            })
            .always(function (resp) {
                if (options.always)
                    options.always(resp);
            })
            .async(options.async === undefined ? true : options.async)
            .update();

        return this;
    };

    /* authorizeCurrentUser
     * options.ability: Ability object
     * options.then: callback to perform action when the request completes successfully.  Parameters are (bool)
     * options.fail: callback to perform an action on a request failure.  Parameters are (error, message)
     * options.always (optional): callback to perform when the request is over (failure or success). Parameters are (response)
     */
    hero.authorizeCurrentUser = function (options) {
        if (!options.ability || !options.ability.abilityName)
            throw "You must provide a ability to the options list.";
        if (!options.then)
            throw "You must provide a then callback to the options list.";
        if (!options.fail)
            throw "You must provide a fail callback to the options list.";

        request.get(instance.options.endpoint + "AuthorizeCurrentUser/" + options.ability.abilityName)
            .then(function (resp) {
                //resp will be a boolean.
                options.then(resp);
            })
            .fail(function (err, msg) {
                options.then(err, msg);
            })
            .always(function (resp) {
                if (options.always)
                    options.always(resp);
            })
            .async(options.async === undefined ? true : options.async)
            .update();

        return this;
    };

    /* getAbilititesForUser
     * options.user: User object
     * options.then: callback to perform action when the request completes successfully.  Parameters are (Ability[])
     * options.fail: callback to perform an action on a request failure.  Parameters are (error, message)
     * options.always (optional): callback to perform when the request is over (failure or success). Parameters are (response)
     */
    hero.getAbilititesForUser = function (options) {

        if (!options.user || !options.user.userName)
            throw "You must provide a user to the options list.";
        if (!options.then)
            throw "You must provide a then callback to the options list.";
        if (!options.fail)
            throw "You must provide a fail callback to the options list.";

        request.get(instance.options.endpoint + "GetAbilitiesForUser/" + options.user.userName)
            .then(function (resp) {
                //deserialize the ability objects
                options.then(deserializeAbilityList(resp));
            })
            .fail(function (err, msg) {
                options.then(err, msg);
            })
            .always(function (resp) {
                if (options.always)
                    options.always(resp);
            })
            .async(options.async === undefined ? true : options.async)
            .update();

        return this;
    };

    /* getAbilitiesForRole
     * options.role: Role object
     * options.then: callback to perform action when the request completes successfully.  Parameters are (Ability[])
     * options.fail: callback to perform an action on a request failure.  Parameters are (error, message)
     * options.always (optional): callback to perform when the request is over (failure or success). Parameters are (response)
     */
    hero.getAbilitiesForRole = function (options) {
        if (!options.role || !options.role.roleName)
            throw "You must provide a role to the options list.";
        if (!options.then)
            throw "You must provide a then callback to the options list.";
        if (!options.fail)
            throw "You must provide a fail callback to the options list.";

        request.get(instance.options.endpoint + "GetAbilitiesForRole/" + options.role.roleName)
            .then(function (resp) {
                options.then(deserializeAbilityList(resp));
            })
            .fail(function (err, msg) {
                options.then(err, msg);
            })
            .always(function (resp) {
                if (options.always)
                    options.always(resp);
            })
            .async(options.async === undefined ? true : options.async)
            .update();

        return this;
    };

    /* getRolesForUser
     * options.user: User object
     * options.then: callback to perform action when the request completes successfully.  Parameters are (Role[])
     * options.fail: callback to perform an action on a request failure.  Parameters are (error, message)
     * options.always (optional): callback to perform when the request is over (failure or success). Parameters are (response)
     */
    hero.getRolesForUser = function (options) {
        if (!options.user || !options.user.userName)
            throw "You must provide a user to the options list.";
        if (!options.then)
            throw "You must provide a then callback to the options list.";
        if (!options.fail)
            throw "You must provide a fail callback to the options list.";

        request.get(instance.options.endpoint + "GetRolesForUser/" + options.user.userName)
            .then(function (resp) {
                options.then(deserializeRoleList(resp));
            })
            .fail(function (err, msg) {
                options.then(err, msg);
            })
            .always(function (resp) {
                if (options.always)
                    options.always(resp);
            })
            .async(options.async === undefined ? true : options.async)
            .update();

        return this;
    };
})(window.Hero = window.Hero || {}, Request);   //Request is a Craft object used for ajax requests and promises

//no conflict mode for craft
$.noConflict()