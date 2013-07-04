(function (hero, angular, undefined) {

  hero.heroApp.controller('NewUserController', function($scope, $location, Restangular, heroOptions) {
    $scope.options = heroOptions.getOptions();
    $scope.roles = Restangular.all('role').getList();
    if (!$scope.user)
      $scope.user = {};
    if (!$scope.user.roles)
        $scope.user.roles = [];
    $scope.filterRoles = function(item) {
      var isFound = false;

      if ($scope.user) {
        angular.forEach($scope.user.roles, function(value, key) {
          if (value.id === item.id) {
            isFound = true;
          }
        });
      }
      if (!isFound) {
        if ($scope.searchRole && $scope.searchRole !== "") {
          return item.name.indexOf($scope.searchRole) >= 0;
        }
        return true;
      }
      return false;
    };
    
    $scope.addRole = function(role) {
      $scope.user.roles.push(role);
    };
    $scope.removeRole = function(idx) {
      $scope.user.roles.splice(idx, 1);
    };
    
    $scope.save = function() {
      $scope.user.id = $scope.user.name;
      
      Restangular.all('user').post($scope.user).then(function(user) {
        $location.path('/users');
      });
    };
  });

  hero.heroApp.controller('EditUserController', function($scope, $location, Restangular, heroOptions, user) {
    var original = user;
    $scope.options = heroOptions.getOptions();

    Restangular.all('role').getList().then(function(items) {
      $scope.roles = items;
    });
    
    $scope.user = Restangular.copy(original);

    $scope.filterRoles = function(item) {
      var isFound = false;
      angular.forEach($scope.user.roles, function(value, key) {
        if (value.id === item.id) {
          isFound = true;
        }
      });

      if (!isFound) {
        if ($scope.searchRole && $scope.searchRole !== "") {
          return item.name.indexOf($scope.searchRole) >= 0;
        }
        return true;
      }
      return false;
    };

    $scope.addRole = function(role) {
      $scope.user.roles.push(role);
    };
    $scope.removeRole = function(idx) {
      $scope.user.roles.splice(idx, 1);
    };
    $scope.isClean = function() {
      return angular.equals(original, $scope.user);
    };

    $scope.destroy = function() {
      original.remove().then(function() {
        $location.path('/users');
      });
    };

    $scope.save = function() {
      $scope.user.put().then(function() {
        $location.path('/users');
      });
    };
  });

  hero.heroApp.controller('UserListController', function($scope, $location, Restangular, heroOptions) {
    $scope.users = Restangular.all('user').getList();
    $scope.options = heroOptions.getOptions();
  });

})(window.Hero = window.Hero || {}, angular);