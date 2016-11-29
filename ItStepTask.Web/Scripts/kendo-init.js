
$(document).ready(function () {
    console.log("kendo init!");
    $("#grid").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: "/api/Shop",
                dataType: "json",
                type : 'GET',
                success: function (data) {
                    alert(data);
                },
                error: function (xhr, error) {
                    console.debug(xhr); console.debug(error);
                }
            },
            pageSize: 10,
            serverPaging: true
        },
        schema: {
            model: {
                fields: {
                    Name: { type: "string" },
                    Price: { type: "number" },
                    Quantity: { type: "number" },
                    //Discontinued: { type: "boolean" }
                }
            }
        },
        pageSize: 20,
        autoBind: true,
        height: 550,
        scrollable: true,
        sortable: true,
        filterable: true,
        pageable: {
            input: true,
            numeric: false
        },
        columns: [
            "Name",
            { field: "Price", title: "Unit Price", format: "{0:c}", width: "130px" },
            { field: "Quantity", title: "Units In Stock", width: "130px" },
            //{ field: "Discontinued", width: "130px" }
        ]
    });
});
