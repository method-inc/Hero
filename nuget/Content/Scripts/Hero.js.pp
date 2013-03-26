(function (hero) {
    //private
    var options = {};
    options.endpoint = "http://localhost/Abilities/";

    var extend = function (optionsToMerge) {
        for (var i in optionsToMerge) {
            if (options.hasOwnProperty(i)) {
                options[i] = optionsToMerge[i];
            }
        }
    };

    //Types
    hero.User = function (name) {
        return { userName: name };
    };

    hero.Role = function (name) {
        return { roleName: name };
    };

    hero.Ability = function (name) {
        return { abilityName: name };
    };

    //public API
    hero.configure = function (mergeOptions) {
        extend(mergeOptions);
        return this;
    };

    hero.getAbilititesForUser = function (user) {
        console.log(user);
        return this;
    };

    hero.getAbilitiesForRole = function (role) {
        console.log(role);
        return this;
    };

    hero.authorizeCurrentUser = function (ability) {
        console.log(ability);
        return this;
    };

    hero.registerAbility = function (ability, action) {
        console.log(ability);
        console.log(action);
        return this;
    };
})(window.Hero = window.Hero || {});