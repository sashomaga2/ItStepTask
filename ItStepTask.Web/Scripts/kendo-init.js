﻿
$(document).ready(function () {
    console.log("kendo init!");
    $("#grid").kendoGrid({
        dataSource: {
            transport: {
                read: "/api/Items",
                dataType: "json",
                type : 'GET'
            },
            pageSize: 5,
            serverPaging: true,
            schema: {
                type: "json",
                data: "data", // records are returned in the "data" field of the response
                total: "total", // total number of records is in the "total" field of the response
                model: {
                    fields: {
                        Id: { type: "number" },
                        Image: { type: "string" },
                        Name: { type: "string" },
                        Price: { type: "string" },
                        Quantity: { type: "number" }
                    }
                }

            },
            serverFiltering: true,
            //filter: { field: "Name", operator: "startswith", value: "" }
        },
        //change: onChange,
        dataBound: onDataBound,
        height: 600,
        
        //serverSorting: true,
        
        scrollable: true,
        sortable: true,
        
        //filterable: true,
        pageable: {
            input: true,
            numeric: false
        },
        filterable: {
            extra: false,
            operators: {
                string: {
                    startswith: "starts with"
                }
            }
        },
        columns: [
            {                                                                                   
                field: "Image", title: "Image", width: "130px", filterable: false, template: "<a href='delailts/#=Id#' class='item-details'><img class='item-image'" +
                                      " src='data:image/png;base64,#=Image#' /></>"
            },
            {
                field: "Name", template: "<a href='delailts/#=Id#' class='item-details'>#=Name#</a>"
            },
            
            {
                field: "Price", title: "Unit Price", /*format: "{0:c}",*/ width: "130px", filterable: false, template : "<div>#=Price#</div>"
            },
            { field: "Quantity", title: "Units In Stock", width: "130px", filterable: false },
            { command: { text: "Order", click: order }, title: " ", width: "100px", filterable: false }
            
        ]
    });

    function order(e) {
        var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
        
        $.ajax({
            type: "PUT",
            url: "/ShoppingCart/Put/" + dataItem.Id,
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                if (response.success) {
                    increaseShoppingCartCount();
                }    
            },
            error: function (xhr, err) {
                alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
                alert("responseText: " + xhr.responseText);
            }
        });

        return false;
    }

    function increaseShoppingCartCount() {
        $el = $('#shopCartCount');
        var count = Number($el.text());
        $el.text(++count);
    }

    var handleDetailsClick = function (e) {
        console.log("handleDetailsClick", e.currentTarget);
        console.log("id", $(e.currentTarget).attr("href").split('/')[1]);

        var id =  $(e.currentTarget).attr("href").split('/')[1];

        $.ajax({
            type: "GET",
            url: "/Items/Details/" + id,
            //contentType: "application/json; charset=utf-8",
            success: function (html) {
                console.log("success", html);
                $('.modal-dialog').html(html);
                $('#myModal').modal('toggle');
            },
            error: function (xhr, err) {
                alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
                alert("responseText: " + xhr.responseText);
            }
        });

        return false;
    }

    function onDataBound() {
        console.log("onDataBound!");
        $('.item-details').click(handleDetailsClick);
    }

    
});






