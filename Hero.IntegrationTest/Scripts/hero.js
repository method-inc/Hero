(function (hero, reqwest) {
    //private instance data
    var heroOptions = {};
    var instance = {};
    
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

    var deserializeAbilityList = function(abilityObjList) {
        var retArray = [];
        for (var index in abilityObjList) {
            retArray.push(deserializeAbility(abilityObjList[index]));
        }

        return retArray;
    };

    var deserializeAbility = function(abilityObj) {
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

    var deserializeRole = function(roleObj) {
        return hero.Role(roleObj.Name);
    };

    //Public Model Types
    hero.User = function (name) {
        return { userName: name };
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
        return this;
    };
    
    hero.registerAbility = function (ability, action) {
        return this;
    };
    
    //public API (actions)

    hero.authorizeUser = function(ability) {

    };

    hero.registerAbility = function(ability, object) {

    };

    /* getAbilititesForUser
     * options.user: User object
     * options.then: callback to perform action when the request completes successfully.  Parameters are (Ability[])
     * options.fail: callback to perform an action on a request failure.  Parameters are (error, message)
     * options.always (optional): callback to perform when the request is over (failure or success). Parameters are (response)
     */
    hero.getAbilititesForUser = function (options) {

        if (!options.user)
            throw "You must provide a user to the options list.";
        if (!options.then)
            throw "You must provide a then callback to the options list.";
        if (!options.fail)
            throw "You must provide a fail callback to the options list.";

        reqwest({
            url: instance.options.endpoint + "GetAbilitiesForUser/" + options.user.userName,
            type: 'json'
        })
            .then(function (resp) {
                //deserialize the ability objects
                options.then(deserializeAbilityList(resp));
            })
            .fail(function(err, msg) {
                options.then(err, msg);
            })
            .always(function(resp) {
                if (options.always)
                    options.always(resp);
            });
        
        return this;
    };

    /* getAbilitiesForRole
     * options.role: Role object
     * options.then: callback to perform action when the request completes successfully.  Parameters are (Ability[])
     * options.fail: callback to perform an action on a request failure.  Parameters are (error, message)
     * options.always (optional): callback to perform when the request is over (failure or success). Parameters are (response)
     */
    hero.getAbilitiesForRole = function (options) {
        if (!options.role)
            throw "You must provide a role to the options list.";
        if (!options.then)
            throw "You must provide a then callback to the options list.";
        if (!options.fail)
            throw "You must provide a fail callback to the options list.";

        reqwest({
            url: instance.options.endpoint + "GetAbilitiesForRole/" + options.role.roleName,
            type: 'json'
        })
            .then(function (resp) {
                options.then(deserializeAbilityList(resp));
            })
            .fail(function (err, msg) {
                options.then(err, msg);
            })
            .always(function (resp) {
                if (options.always)
                    options.always(resp);
            });

        return this;
    };

    /* getRolesForUser
     * options.user: User object
     * options.then: callback to perform action when the request completes successfully.  Parameters are (Role[])
     * options.fail: callback to perform an action on a request failure.  Parameters are (error, message)
     * options.always (optional): callback to perform when the request is over (failure or success). Parameters are (response)
     */
    hero.getRolesForUser = function (options) {
        if (!options.user)
            throw "You must provide a user to the options list.";
        if (!options.then)
            throw "You must provide a then callback to the options list.";
        if (!options.fail)
            throw "You must provide a fail callback to the options list.";

        reqwest({
            url: instance.options.endpoint + "GetRolesForUser/" + options.user.userName,
            type: 'json'
        })
            .then(function (resp) {
                options.then(deserializeRoleList(resp));
            })
            .fail(function (err, msg) {
                options.then(err, msg);
            })
            .always(function (resp) {
                if (options.always)
                    options.always(resp);
            });

        return this;
    };
})(window.Hero = window.Hero || {}, reqwest);