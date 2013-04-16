//Add a module for the buttons on the page.
(function (testModule) {
    var getCreateButton = function() { return document.getElementsByClassName("createButton"); };
    var getDeleteButton = function () { return document.getElementsByClassName("deleteButton"); };
    var getEditButton = function () { return document.getElementsByClassName("editButton"); };
    var getDetailsButton = function () { return document.getElementsByClassName("detailsButton"); };

    var show = function (elem) {
        if (elem) {
            for (var i = 0; i < elem.length; i++) {
                if (elem[i]) elem[i].style.display = 'inline';
            }
        }
    };
    
    testModule.showCreateButton = function showCreateButton() {
        show(getCreateButton());
    };
    
    testModule.showDeleteButton = function showDeleteButton() {
        show(getDeleteButton());
    };
    
    testModule.showEditButton = function showEditButton() {
        show(getEditButton());
    };
    
    testModule.showDetailsButton = function showDetailsButton() {
        show(getDetailsButton());
    };
    
})(window.TestModule = window.TestModule || {});

//on doc ready show/hide the buttons.
$(document).ready(function () {
    TestModule.showCreateButton();
    TestModule.showDeleteButton();
    TestModule.showEditButton();
    TestModule.showDetailsButton();
});


//initialize the ability based authorization utilizing the Hero project
Hero
    .configure({ endpoint: "http://localhost:54573/Abilities/" })
    .registerAbility(Hero.Ability("View"), TestModule, TestModule.showDetailsButton)
    .registerAbility(Hero.Ability("Create"), TestModule, TestModule.showCreateButton)
    .registerAbility(Hero.Ability("Edit"), TestModule, TestModule.showEditButton)
    .registerAbility(Hero.Ability("Delete"), TestModule, TestModule.showDeleteButton);