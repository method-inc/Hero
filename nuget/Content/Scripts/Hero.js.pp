(function (hero, angular, undefined) {
  var heroOptions = {};
  
  heroOptions.userListTemplatePath = "/Templates/user-list.html";
  heroOptions.userEditTemplatePath = "/Templates/user-create.html";
  heroOptions.userNewTemplatePath = "/Templates/user-create.html";
  heroOptions.roleListTemplatePath = "/Templates/role-list.html";
  heroOptions.roleEditTemplatePath = "/Templates/role-create.html";
  heroOptions.roleNewTemplatePath = "/Templates/role-create.html";
  heroOptions.abilityListTemplatePath = "/Templates/ability-list.html";
  heroOptions.abilityEditTemplatePath = "/Templates/ability-create.html";
  heroOptions.abilityNewTemplatePath = "/Templates/ability-create.html";
  heroOptions.headerTemplatePath = "/Templates/header.html";
  heroOptions.adminConsoleInjectionId = "admin-console";
  heroOptions.apiBaseUrl = "api/";

  hero.configure = function (options) {
    angular.extend(heroOptions, options);
    return this;
  };

  hero.init = function() {
    document.getElementsByTagName('html')[0].setAttribute('ng-app', 'heroApp');
    document.getElementsByTagName('body')[0].setAttribute('ng-controller', 'CurrentUserController');
    return this;
  };
  
  hero.buildConsole = function() {
    document.getElementById(heroOptions.adminConsoleInjectionId).setAttribute('ng-view', '');
    return this;
  };

  hero.heroApp = angular.module('heroApp', ['restangular']);

  hero.heroApp.factory("heroOptions", function() {
    return {
      getOptions: function() {
        return heroOptions;
      }
    };
  });

  hero.heroApp.config(function($routeProvider, RestangularProvider) {
    RestangularProvider.setBaseUrl(heroOptions.apiBaseUrl);
    
    $routeProvider
      .when('/users', { templateUrl: heroOptions.userListTemplatePath, controller: 'UserListController' })
      .when('/edituser/:userId', {
        templateUrl: heroOptions.userEditTemplatePath,
        controller: 'EditUserController',
        resolve: {
          user: function(Restangular, $route) {
            return Restangular.one('user', $route.current.params.userId).get();
          }
        }
      })
      .when('/newuser', { templateUrl: heroOptions.userNewTemplatePath, controller: 'NewUserController' })
      .when('/roles', { templateUrl: heroOptions.roleListTemplatePath, controller: 'RoleListController' })
      .when('/editrole/:roleId', {
        templateUrl: heroOptions.roleEditTemplatePath,
        controller: 'EditRoleController',
        resolve: {
          role: function(Restangular, $route) {
            return Restangular.one('role', $route.current.params.roleId).get();
          }
        }
      })
      .when('/newrole', { templateUrl: heroOptions.roleNewTemplatePath, controller: 'NewRoleController' })
      .when('/abilities', { templateUrl: heroOptions.abilityListTemplatePath, controller: 'AbilityListController' })
      .when('/editability/:abilityId', {
        templateUrl: heroOptions.abilityEditTemplatePath,
        controller: 'EditAbilityController',
        resolve: {
          ability: function(Restangular, $route) {
            return Restangular.one('ability', $route.current.params.abilityId).get();
          }
        }
      })
      .when('/newability', { templateUrl: heroOptions.abilityNewTemplatePath, controller: 'NewAbilityController' })
      .otherwise({ redirectTo: '/users' });
  });

  hero.heroApp.controller('NavbarController', function($scope, $location) {

    $scope.routeContains = function(routeName) {
      return $location.path().indexOf(routeName) >= 0;
    };

  });
})(window.Hero = window.Hero || {}, angular);