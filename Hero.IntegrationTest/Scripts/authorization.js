//Add some "module" code to hide an element on the page

(function (testModule) {
    var elementToDisplay = function() { return document.getElementById("testAbilityElement"); };

    testModule.show = function show() {
        elementToDisplay().style.display = 'inline';
    };
    
})(window.TestModule = window.TestModule || {});

//initialize the ability based authorization utilizing the Hero project
Hero
    .configure({ endpoint: "http://localhost:54573/Abilities/" })
    .registerAbility(Hero.Ability("View"), TestModule, TestModule.show);

