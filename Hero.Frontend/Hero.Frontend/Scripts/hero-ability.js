(function (hero, angular, undefined) {

  hero.heroApp.controller('NewAbilityController', function($scope, $location, Restangular, heroOptions) {
    $scope.options = heroOptions.getOptions();
    $scope.abilities = Restangular.all('ability').getList();
    if (!$scope.ability)
      $scope.ability = {};
    if (!$scope.ability.abilities)
        $scope.ability.abilities = [];
    $scope.filterAbilities = function(item) {
      var isFound = false;
      
      if ($scope.ability) {
        angular.forEach($scope.ability.abilities, function(value, key) {
          if (value.id === item.id) {
            isFound = true;
          }
        });
      }

      $scope.addAbility = function(ability) {
        $scope.ability.abilities.push(ability);
      };
      $scope.removeAbility = function(idx) {
        $scope.ability.abilities.splice(idx, 1);
      };

      if (!isFound) {
        if ($scope.searchAbility && $scope.searchAbility !== "") {
          return item.name.indexOf($scope.searchAbility) >= 0;
        }
        return true;
      }
      return false;
    };

    $scope.save = function() {
      $scope.ability.id = $scope.ability.name;
      
      Restangular.all('ability').post($scope.ability).then(function(ability) {
        $location.path('/abilities');
      });
    };
  });

  hero.heroApp.controller('EditAbilityController', function($scope, $location, Restangular, heroOptions, ability) {
    var original = ability;
    $scope.options = heroOptions.getOptions();

    $scope.abilities = Restangular.all('ability').getList();
    
    $scope.ability = Restangular.copy(original);

    $scope.filterAbilities = function(item) {
      var isFound = false;
      angular.forEach($scope.ability.abilities, function(value, key) {
        if (value.id === item.id) {
          isFound = true;
        }
      });

      if (!isFound) {
        if ($scope.searchAbility && $scope.searchAbility !== "") {
          return item.name.indexOf($scope.searchAbility) >= 0;
        }
        return true;
      }
      return false;
    };

    $scope.addAbility = function(ability) {
      $scope.ability.abilities.push(ability);
    };
    $scope.removeAbility = function(idx) {
      $scope.ability.abilities.splice(idx, 1);
    };
    $scope.isClean = function() {
      return angular.equals(original, $scope.ability);
    };

    $scope.destroy = function() {
      original.remove().then(function() {
        $location.path('/abilities');
      });
    };

    $scope.save = function() {
      $scope.ability.put().then(function() {
        $location.path('/abilities');
      });
    };
  });

  hero.heroApp.controller('AbilityListController', function($scope, $location, Restangular, heroOptions) {
    $scope.abilities = Restangular.all('ability').getList();
    $scope.options = heroOptions.getOptions();
  });

})(window.Hero = window.Hero || {}, angular);