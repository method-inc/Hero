(function (hero, angular, undefined) {
  var userAbilities;
  
  hero.heroApp.controller('CurrentUserController', function($scope, Restangular) {
    Restangular.all('authorization').getList().then(function(items) {
      userAbilities = items;
    });
  });
  
  /**
      * The registration can take a true object or a function as the second paramter
      * If only an object and no function are passed, it will register itself with every function off the object
      */
  hero.registerAbility = function(abilityName, target, fn, functionName) {
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

    target.prototype[functionName] = function() {
      var that = this;
      var thatArguments = arguments;

      var foundAbility = _.find(userAbilities, function (item) { return item && item.name === abilityName; });
      var isAuthorized = foundAbility !== undefined && foundAbility !== null;
      

      if (isAuthorized) {
        return fn.apply(that, thatArguments);
      } else {
        return "UserNotAuthorized";
      }
    };

    //setup the option of re-registering
    target.prototype[functionName].trueName = fn.name;
    target.prototype[functionName].originalFunction = fn;

    return this;
  };
})(window.Hero = window.Hero || {}, angular);