<h1>Users</h1>

<input type="text" ng-model="searchUser" placeholder="Search Users..." />

<table>
    <thead>
        <tr>
            <th>Name</th>
            <th><a href="#/newuser">Add</a></th>
        </tr>
    </thead>
    <tbody>
        <tr ng-repeat="user in users | filter:searchUser | orderBy:'name'">
            <td>{{user.name}}</td>
            <td>
                <a href="#/edituser/{{user.id}}">Edit</a>
            </td>
        </tr>
    </tbody>
</table>
