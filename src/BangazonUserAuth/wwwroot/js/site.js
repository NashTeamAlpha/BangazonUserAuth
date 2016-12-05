$(document).ready(function () {
//This method waits for the Category select drop down in the add new product form to be changed- when it is- this method gets all SubProductTypes inside the selected category and populates the subcategory drop down list thats just below the select category drop down list in the add product form. 
    $("#ProductTypesList").on("change", function(e) {
        $.ajax({
            url: `/Products/GetSubTypes/${$(this).val()}`,
            method: "POST",
            dataType: "json",
            contentType: 'application/json; charset=utf-8'
        }).done((subTypes) => {
            $("#Product_SubProductTypeId").html("");
            $("#Product_SubProductTypeId").append("<option value=null> Choose a Sub Category </option>");
            subTypes.forEach((option) => {
                console.log(option);
                $("#Product_SubProductTypeId").append(`<option value="${option.subProductTypeId}">${option.name}</option>`);
            });
        });
    });
// Adds an item to the users active shopping cart. Then redirects the user to the index file.    
    $("#AddToCart").on("click", function(e) {
        $.ajax({
            url: `/Products/AddToCart/${$(this).val()}`,
            method: "POST",
            dataType: "json",
            contentType: 'application/json; charset=utf-8'
        }).done((id) => {
            console.log(id);
            window.location.replace("http://localhost:5000");
        });
    });
//Submits order then routes to order completed page
    $("#submitOrder").on("click", function(e){
        $.ajax({
            url: `/Customers/SubmitOrder/${$("#ListOfPaymentTypes").val()}`,
            method: "PATCH",
            dataType: "json",
            contentType: 'application/json; charset=utf-8'
        }).done((id) => {
            console.log(id);
            window.location.replace("http://localhost:5000/Customers/OrderCompleted");
        });
    });
});
