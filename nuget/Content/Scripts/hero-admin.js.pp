//undefined is a parameter that nothing is passed into
//to protect it from being overwritten by another library
//in the global namespace.
(function (heroAdmin, request, undefined) {
  //private instance data
  var heroAdminOptions = {};
  var instance = {};

  heroAdminOptions.userEndPoint = "http://localhost:59116/User/";
  heroAdminOptions.roleEndPoint = "http://localhost:59116/Role/";
  heroAdminOptions.abilityEndPoint = "http://localhost:59116/Ability/";
  instance.options = heroAdminOptions;

  heroAdmin.buildConsole = function(element) {
    var user = new User();

    var form = new Backbone.Form({
      model: user
    }).render();

    element.parentNode.insertBefore(form.el, element.nextSibling);
  };

})(window.HeroAdmin = window.HeroAdmin || {}, Request);   //Request is a Craft object used for ajax requests and promises

//no conflict mode for craft
$.noConflict()