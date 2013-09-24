(function (hero, angular, undefined) {

  hero.heroApp.controller('NewRoleController', function($scope, $location, Restangular, heroOptions) {
    $scope.options = heroOptions.getOptions();
    $scope.abilities = Restangular.all('ability').getList();
    if (!$scope.role)
      $scope.role = {};
    if (!$scope.role.abilities)
        $scope.role.abilities = [];
    $scope.filterAbilities = function(item) {
      var isFound = false;

      if ($scope.role) {
        angular.forEach($scope.role.abilities, function(value, key) {
          if (value.id === item.id) {
            isFound = true;
          }
        });
      }

      if (!isFound) {
        if ($scope.searchAbility && $scope.searchAbility !== "") {
          return item.name.indexOf($scope.searchAbility) >= 0;
        }
        return true;
      }
      return false;
    };
    
    $scope.addAbility = function(ability) {
      $scope.role.abilities.push(ability);
    };
    $scope.removeAbility = function(idx) {
      $scope.role.abilities.splice(idx, 1);
    };

    $scope.save = function() {
      $scope.role.id = $scope.role.name;
      
      Restangular.all('role').post($scope.role).then(function(role) {
        $location.path('/roles');
      });
    };
  });

  hero.heroApp.controller('EditRoleController', function($scope, $location, Restangular, heroOptions, role) {
    var original = role;
    $scope.options = heroOptions.getOptions();
    $scope.abilities = Restangular.all('ability').getList();

    $scope.role = Restangular.copy(original);

    $scope.filterAbilities = function(item) {
      var isFound = false;
      angular.forEach($scope.role.abilities, function(value, key) {
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
      $scope.role.abilities.push(ability);
    };
    $scope.removeAbility = function(idx) {
      $scope.role.abilities.splice(idx, 1);
    };
    $scope.isClean = function() {
      return angular.equals(original, $scope.role);
    };

    $scope.destroy = function() {
      original.remove().then(function() {
        $location.path('/roles');
      });
    };

    $scope.save = function() {
      $scope.role.put().then(function() {
        $location.path('/roles');
      });
    };
  });

  hero.heroApp.controller('RoleListController', function($scope, $location, Restangular, heroOptions) {
    $scope.roles = Restangular.all('role').getList();
    $scope.options = heroOptions.getOptions();
  });

})(window.Hero = window.Hero || {}, angular);