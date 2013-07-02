<form name="myForm">
    <div class="control-group" ng-class="{error: myForm.name.$invalid}">
        <label>name</label>
        <input type="text" name="name" ng-model="user.name" required>
        <span ng-show="myForm.name.$error.required" class="help-inline">Required</span>
    </div>

    <br>
    <a href="#/" class="btn">Cancel</a>
    <button ng-click="save()" ng-disabled="isClean() || myForm.$invalid"
        class="btn btn-primary">
        Save</button>
    <button ng-click="destroy()"
        ng-show="user.id" class="btn btn-danger">
        Delete</button>
</form>
