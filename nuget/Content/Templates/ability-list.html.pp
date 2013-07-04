<div ng-include src="options.headerTemplatePath"></div>

<input type="text" ng-model="searchAbility" placeholder="Search Abilities..." class="search-query" />

<table class="table table-hover">
    <thead>
        <tr>
            <th>Name</th>
            <th>
                <a href="#/newability"><i class="icon-plus"></i></a>
            </th>
        </tr>
    </thead>
    <tbody>
        <tr ng-repeat="ability in abilities | filter:searchAbility | orderBy:'name'">
            <td>{{ability.name}}</td>
            <td>
                <a href="#/editability/{{ability.id}}"><i class="icon-folder-open"></i></a>
            </td>
        </tr>
    </tbody>
</table>
