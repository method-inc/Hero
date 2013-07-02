(function (heroAdmin, angular, undefined) {
  //private instance data
  var heroOptions = {};
  var instance = {};

  heroOptions.userListTemplatePath = "/Templates/user-list.html";
  heroOptions.userEditTemplatePath = "/Templates/user-create.html";
  heroOptions.userNewTemplatePath = "/Templates/user-create.html";
  heroOptions.injectionLocationId = "admin-console";
  heroOptions.apiBaseUrl = "api/";
  instance.options = heroOptions;

  //private functions
  var extend = function (optionsToMerge) {
    for (var i in optionsToMerge) {
      if (heroOptions.hasOwnProperty(i)) {
        heroOptions[i] = optionsToMerge[i];
      }
    }
  };

  angular.module('hero.user', ['restangular'])
    .config(function($routeProvider, RestangularProvider) {
      RestangularProvider.setBaseUrl(instance.options.apiBaseUrl);
      $routeProvider
        .when('/users', { templateUrl: instance.options.userListTemplatePath, controller: 'UserListController' })
        .when('/edituser/:userId', {
          templateUrl: instance.options.userEditTemplatePath,
          controller: 'EditUserController',
          resolve: {
            user: function(Restangular, $route) {
              return Restangular.one('user', $route.current.params.userId).get();
            }
          }
        })
        .when('/newuser', { templateUrl: instance.options.userNewTemplatePath, controller: 'NewUserController' })
        .otherwise({ redirectTo: '/users' });
    })
    .controller('NewUserController', function($scope, $location, Restangular) {
      $scope.save = function() {
        Restangular.all('user').post($scope.user).then(function(user) {
          $location.path('/users');
        });
      };
    })
    .controller('EditUserController', function($scope, $location, Restangular, user) {
      var original = user;
      $scope.user = Restangular.copy(original);

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
    })
    .controller('UserListController', function($scope, Restangular) {
      $scope.users = Restangular.all('user').getList();
    });

  //public API (configuration)
  heroAdmin.configure = function (options) {
    extend(options);
    instance.options = heroOptions;
    return this;
  };

  heroAdmin.buildConsole = function () {
    document.getElementsByTagName('html')[0].setAttribute('ng-app', 'hero.user');
    document.getElementById(instance.options.injectionLocationId).setAttribute('ng-view', '');
    return this;
  };

})(window.HeroAdmin = window.HeroAdmin || {}, angular);