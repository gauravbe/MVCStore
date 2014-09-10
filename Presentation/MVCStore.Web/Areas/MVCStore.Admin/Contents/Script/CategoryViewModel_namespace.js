/// <reference path="../../Content/scripts/jquery-1.9.1.min.js" />
/// <reference path="../../Content/scripts/knockout-2.1.0.js" />

$(document).ready(function () {
    categoryViewModelNamespace.viewModel = null;
    categoryViewModelNamespace.loadPage(1, 5);
    $(document).delegate("#prevPageButton", "click", categoryViewModelNamespace.previousPage);
    $(document).delegate("#nextPageButton", "click", categoryViewModelNamespace.nextPage);
    $(document).delegate("#rowsPerPage", "change", categoryViewModelNamespace.pageSizeChanged);
});

// Initialized the namespace
var categoryViewModelNamespace = {};

ko.validation.rules.pattern.message = 'Invalid.';


ko.validation.configure({
    registerExtenders: true,
    messagesOnModified: true,
    insertMessages: true,
    parseInputAttributes: true,
    messageTemplate: null
});

categoryViewModelNamespace.initViewModel = function (pushModel) {

    categoryViewModelNamespace.recordCount = ko.observable(pushModel.RecordCount);
    categoryViewModelNamespace.pageSize = ko.observable(pushModel.PageSize);
    categoryViewModelNamespace.currentPage = ko.observable(pushModel.PageNumber);
    categoryViewModelNamespace.pageData = ko.observableArray(pushModel.Data);

    this.ID = ko.observable();
    this.Name = ko.observable().extend({ required: "Please enter a Category Name" });
    this.Description = ko.observable().extend({ required: "Please enter a Category Description"  });


    var category = {
        ID: this.ID,
        Name: this.Name,
        Description: this.Description
    }

    categoryViewModelNamespace.ID = this.ID;
    categoryViewModelNamespace.Name = this.Name;
    categoryViewModelNamespace.Description = this.Description;
    categoryViewModelNamespace.Category = this.category;

    var viewModel = ko.observable(
        {
            pageSize: categoryViewModelNamespace.pageSize,
            currentPage: categoryViewModelNamespace.currentPage,
            pageData: categoryViewModelNamespace.pageData,
            recordCount: categoryViewModelNamespace.recordCount,
            Category: category
        }
    );
    categoryViewModelNamespace.category = ko.observable();    

    return viewModel;
}

categoryViewModelNamespace.categoryViewModel = function (category) {

    var validationOptions =
      { insertMessages: true, decorateElement: true, errorElementClass: 'errorFill' };
    ko.validation.init(validationOptions);

    categoryViewModelNamespace.category(category);
}

// Bind the customer
categoryViewModelNamespace.bindData = function (pushModel) {

    /** If we don't check for null logic then it throws 
    ***** Uncaught Error: You cannot apply bindings multiple times to the same element. **********
    error **/

    // Create the view model
    if (categoryViewModelNamespace.viewModel == null) {
        categoryViewModelNamespace.viewModel =
            categoryViewModelNamespace.initViewModel(pushModel);        
        ko.validation.configure();        
        ko.applyBindings(this.viewModel);
    } else {
        categoryViewModelNamespace.viewModel =
           categoryViewModelNamespace.pageData(ko.observableArray());
        categoryViewModelNamespace.viewModel =
           categoryViewModelNamespace.pageData(pushModel.Data);
    }
    this.viewModel.errors = ko.validation.group(this.viewModel);
}

categoryViewModelNamespace.loadPage = function (pageNumber, pageSize) {

    // Initialize the view-model
    $.ajax({
        url: "/Category/GetAllCategories?pageNumber=" + pageNumber + "&pageSize=" + pageSize,
        cache: false,
        type: 'GET',
        contentType: 'application/json; charset=utf-8',
        data: {},
        success: function (data) {
            categoryViewModelNamespace.bindData(data);
        }
    });
}

categoryViewModelNamespace.addCategory = function () {

    //var errors = ko.validation.group(categoryViewModelNamespace.viewModel);
    if (categoryViewModelNamespace.viewModel.errors().length == 0) {
        $.ajax({
            url: '/Category/Add/',
            cache: false,
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            data: ko.toJSON(this.Category),
            success: function(data) {
                categoryViewModelNamespace.pageData.push(data);
                categoryViewModelNamespace.Name("");
                categoryViewModelNamespace.Description("");
                categoryViewModelNamespace.loadPage(categoryViewModelNamespace.currentPage(),
                    categoryViewModelNamespace.pageSize());
            }
        }).fail(
            function(xhr, textStatus, err) {
                alert(err);
            });
    } else {
        this.viewModel.errors.showAllMessages();
    }
}

categoryViewModelNamespace.edit = function(category) {
    categoryViewModelNamespace.categoryViewModel(category);
}

categoryViewModelNamespace.update = function() {
    var category = categoryViewModelNamespace.category();

    $.ajax({
            url: '/Category/Update/',
            cache: false,
            type: 'PUT',
            contentType: 'application/json; charset=utf-8',
            data: ko.toJSON(category),
            success: function(data) {
                categoryViewModelNamespace.pageData.removeAll();
                categoryViewModelNamespace.pageData(data); //Put the response in ObservableArray
                categoryViewModelNamespace.category(null);
                alert("Record Updated Successfully");

            }
        })
        .fail(
            function(xhr, textStatus, err) {
                alert(err);
            });
}


categoryViewModelNamespace.delete = function (Category) {
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
                    categoryViewModelNamespace.pageData.remove(Category);
                    alert("Record Deleted Successfully");
                }
            }).fail(
             function (xhr, textStatus, err) {
                 alert(err);
             });
        }
    }


categoryViewModelNamespace.isLastPage = function () {
    var recordsLeft = (categoryViewModelNamespace.recordCount() -
                     (categoryViewModelNamespace.pageSize() *
                     categoryViewModelNamespace.currentPage()));
    return recordsLeft <= 0;
}

categoryViewModelNamespace.isFirstPage = function () {
    return categoryViewModelNamespace.currentPage() == 1;
}

categoryViewModelNamespace.previousPage = function (data) {
    if (!categoryViewModelNamespace.isFirstPage()) {
        categoryViewModelNamespace.currentPage(
            categoryViewModelNamespace.currentPage() - 1);
        categoryViewModelNamespace.loadPage(categoryViewModelNamespace.currentPage(), categoryViewModelNamespace.pageSize());
    }
}

categoryViewModelNamespace.nextPage = function (data) {
    if (!categoryViewModelNamespace.isLastPage()) {
        categoryViewModelNamespace.currentPage(
            categoryViewModelNamespace.currentPage() + 1);
        categoryViewModelNamespace.loadPage(categoryViewModelNamespace.currentPage(),
            categoryViewModelNamespace.pageSize());
    }
}

categoryViewModelNamespace.pageSizeChanged = function (data) {
    if (categoryViewModelNamespace.pageSize() != $("#rowsPerPage").val()) {
        categoryViewModelNamespace.pageSize($("#rowsPerPage").val());
        categoryViewModelNamespace.currentPage(1);
        categoryViewModelNamespace.loadPage(categoryViewModelNamespace.currentPage(),
             categoryViewModelNamespace.pageSize());
    }
}