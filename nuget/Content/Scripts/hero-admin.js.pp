//undefined is a parameter that nothing is passed into
//to protect it from being overwritten by another library
//in the global namespace.
(function (heroAdmin, request, undefined) {
    //private instance data
    var heroOptions = {};
    var instance = {};
    var currentUser = {};

    heroOptions.endpoint = "http://localhost/Authorization/";
    instance.options = heroOptions;

    //private functions
    var extend = function (optionsToMerge) {
        for (var i in optionsToMerge) {
            if (heroOptions.hasOwnProperty(i)) {
                heroOptions[i] = optionsToMerge[i];
            }
        }
    };

    /* getRolesForUser
     * options.user: User object
     * options.then: callback to perform action when the request completes successfully.  Parameters are (Role[])
     * options.fail: callback to perform an action on a request failure.  Parameters are (error, message)
     * options.always (optional): callback to perform when the request is over (failure or success). Parameters are (response)
     */
    heroAdmin.getRolesForUser = function (options) {
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
})(window.HeroAdmin = window.HeroAdmin || {}, Request);   //Request is a Craft object used for ajax requests and promises

//no conflict mode for craft
$.noConflict()