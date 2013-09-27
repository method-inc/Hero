<div ng-include src="options.headerTemplatePath"></div>

<input type="text" ng-model="searchRole" placeholder="Search Roles..." class="search-query" />

<table class="table table-hover">
    <thead>
        <tr>
            <th>Name</th>
            <th>
                <a href="#/newrole"><i class="icon-plus"></i></a>
            </th>
        </tr>
    </thead>
    <tbody>
        <tr ng-repeat="role in roles | filter:searchRole | orderBy:'name'">
            <td>{{role.name}}</td>
            <td>
                <a href="#/editrole/{{role.id}}"><i class="icon-folder-open"></i></a>
            </td>
        </tr>
    </tbody>
</table>
