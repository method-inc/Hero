<div ng-include src="options.headerTemplatePath"></div>

<form name="abilityForm" class="form-search form-horizontal">

    <div class="row">
        <div class="span6">
            <div class="control-group" ng-class="{error: abilityForm.name.$invalid}">
                <label>Ability Name</label>
                <input type="text" name="name" ng-model="ability.name" required>
                <span ng-show="abilityForm.name.$error.required" class="help-inline">Required</span>
            </div>
            <div class="control-group">
                <label>Current Abilities</label>
                <ul>
                    <li ng-repeat="ability in ability.abilities | orderBy:'name'">
                        <span>{{ability.name}}</span>
                        <i ng-click="removeAbility($index)" class="icon-remove"></i>
                    </li>
                </ul>
            </div>
        </div>
        <div class="span6">
            <div class="control-group">
                <label></label>
                <input type="text" ng-model="searchAbility" placeholder="Filter Abilities..." class="search-query" />
            </div>
            <div class="control-group">
                <label>Available Abilities</label>
                <ul>
                    <li ng-repeat="ability in abilities | filter:filterAbilities | orderBy:'name'">
                        <span>{{ability.name}}</span>
                        <i ng-click="addAbility(ability)" class="icon-plus"></i>
                    </li>
                </ul>
            </div>
        </div>
    </div>

    <br>
    <a href="#/abilities" class="btn">Cancel</a>
    <button ng-click="save()" ng-disabled="isClean() || abilityForm.$invalid"
        class="btn btn-primary">
        Save</button>
    <button ng-click="destroy()"
        ng-show="ability.id" class="btn btn-danger">
        Delete</button>
</form>
