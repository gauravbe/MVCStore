/// <reference path="../../Content/scripts/jquery-1.9.1.min.js" />
/// <reference path="../../Content/scripts/knockout-2.1.0.js" />

$(document).ready(function () {
    vm = null;
    loadPage(1, 5);
    $(document).delegate("#prevPageButton", "click", previousPage);
    $(document).delegate("#nextPageButton", "click", nextPage);
    $(document).delegate("#rowsPerPage", "change", pageSizeChanged);
});

ko.extenders.requiredField = function (target, overrideMessage) {
    //add some sub-observables to our observable
    target.hasError = ko.observable();
    target.validationMessage = ko.observable();

    //define a function to do validation
    function validate(newValue) {
        target.hasError(newValue ? false : true);
        target.validationMessage(newValue ? "" : overrideMessage || "This field is required.");
    }

    //initial validation
    validate(target());

    //validate whenever the value changes
    target.subscribe(validate);

    //return the original observable
    return target;
};

ko.extenders.logChange = function (target, option) {
    target.subscribe(function (newValue) {
        console.log(option + ": " + newValue);
    });
    return target;
};

var loadPage = function (pageNumber, pageSize) {

    // Initialize the view-model
    $.ajax({
        url: "/Category/GetAllCategories?pageNumber=" + pageNumber + "&pageSize=" + pageSize,
        cache: false,
        type: 'GET',
        contentType: 'application/json; charset=utf-8',
        data: {},
        success: function (data) {

            if (vm == null) {
                vm = new CategoryViewModel(data);
                vm.errors = ko.validation.group(vm);
                ko.applyBindings(vm);
                return data.Data.length;
            }
            else {
                vm.pageData(ko.observableArray());
                vm.pageData(data.Data);
                vm.errors = ko.validation.group(vm);
                return data.Data.length;
            }            
        }
    });    
}

var previousPage = function (data) {
    if (!vm.isFirstPage()) {
        vm.currentPage(vm.currentPage() - 1);
        loadPage(vm.currentPage(), vm.pageSize());
    }
}

var nextPage = function (data) {
    if (!vm.isLastPage()) {
        vm.currentPage(vm.currentPage() + 1);
        loadPage(vm.currentPage(), vm.pageSize());
    }
}

var pageSizeChanged = function (data) {
    if (vm.pageSize() != $("#rowsPerPage").val()) {
        vm.pageSize($("#rowsPerPage").val());
        vm.currentPage(1);
        loadPage(vm.currentPage(), vm.pageSize());
    }
}

var CategoryViewModel = function (data) {

    //Declare observable which will be bind with UI 
    this.ID = ko.observable("");
    this.Name = ko.observable("").extend({ required: {message: "Please enter Category Name"}, logChange: "Category Name"});
    this.Description = ko.observable("").extend({ required: { message: "Please enter Description Name" } });

    var Category = {
        ID: this.ID,
        Name: this.Name,
        Description: this.Description
    };

    this.Category = ko.observable();
    this.pageSize = ko.observable(data.PageSize);
    this.currentPage = ko.observable(data.PageNumber);
    this.pageData = ko.observableArray(data.Data);
    this.recordCount = ko.observable(data.RecordCount);

    this.isLastPage = function () {
        var recordsLeft = (this.recordCount() - (this.pageSize() * this.currentPage()));
        return recordsLeft <= 0;
    }

    this.isFirstPage = function () {
        return this.currentPage() == 1;
    }

    this.addCategory = function () {
    
        if (vm.errors().length == 0) {           
            $.ajax({
                url: '/Category/Add/',
                cache: false,
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                data: ko.toJSON(Category),
                success: function(data) {
                    // alert('added');
                    vm.pageData.push(data);
                    vm.Name("");
                    vm.Description("");
                    vm.loadPage(vm.currentPage(), vm.pageSize());
                }
            }).fail(
                function(xhr, textStatus, err) {
                    alert(err);
                });

        } else {            
            vm.errors.showAllMessages();
        }
    }

    // Delete product details
    this.delete = function (Category) {
        if (confirm('Are you sure to Delete "' + Category.Name + '" category ??')) {
            var catId = Category.ID;

            $.ajax({
                url: '/Category/Delete/',
                cache: false,
                type: 'GET',
                contentType: 'application/json; charset=utf-8',
                data: { "id": catId },
                datatype: "json",
                success: function (data) {
                    vm.pageData.remove(Category);
                    loadPage(vm.currentPage(), vm.pageSize());
                    alert("Record Deleted Successfully");
                }
            }).fail(
             function (xhr, textStatus, err) {
                 alert(err);
             });
        }
    }

    this.update = function () {
        var category = vm.Category();

        $.ajax({
            url: '/Category/Update/',
            cache: false,
            type: 'PUT',
            contentType: 'application/json; charset=utf-8',
            data: ko.toJSON(category),
            success: function (data) {
                vm.pageData.removeAll();
                vm.pageData(data); //Put the response in ObservableArray
                vm.Category(null);
                loadPage(vm.currentPage(), vm.pageSize());    
            }
        })
    .fail(
    function (xhr, textStatus, err) {
        alert(err);
    });
    }

    this.edit = function (category) {
        vm.Category(category);

    }
}