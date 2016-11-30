
$(document).ready(function () {
    console.log("kendo init!");
    $("#grid").kendoGrid({
        dataSource: {
            transport: {
                read: "/api/Items",
                dataType: "json",
                type : 'GET',
                success: function (data) {
                    alert(data);
                },
                error: function (xhr, error) {
                    console.debug(xhr); console.debug(error);
                }
            },
            pageSize: 5,
            serverPaging: true,
            schema: {
                type: "json",
                data: "data", // records are returned in the "data" field of the response
                total: "total", // total number of records is in the "total" field of the response
                model: {
                    fields: {
                        Image: { type: "string" },
                        Name: { type: "string" },
                        Price: { type: "number" },
                        Quantity: { type: "number" }

                        //Discontinued: { type: "boolean" }
                    }
                }

            },
        },
        height: 550,
        scrollable: true,
        sortable: true,
        filterable: true,
        pageable: {
            input: true,
            numeric: false
        },
        columns: [
            {
                field: "Image", title: "Image", width: "130px", template: "<img class='item-image'" +
                                      " src='data:image/png;base64,#=Image#' />"
            }, 
            "Name",
            { field: "Price", title: "Unit Price", format: "{0:c}", width: "130px" },
            { field: "Quantity", title: "Units In Stock", width: "130px" },
            { command: { text: "Order", click: order }, title: " ", width: "100px" }
            //{ field: "Discontinued", width: "130px" }
        ]
    });

    function order(e) {
        e.preventDefault();

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
    }

    function increaseShoppingCartCount() {
        $el = $('#shopCartCount');
        var count = Number($el.text());
        $el.text(++count);
    }
});






