$(function () {
    var ajaxCategoryChange = function () {
        var url = "api/Shop";

        console.log("this", $(this).val());

        //$.ajax({
        //    type: "POST",
        //    data: JSON.stringify(customer),
        //    url: "api/Customer",
        //    contentType: "application/json"
        //});

        $.ajax({
            type: 'POST',
            url: url,
            dataType: 'json',
            data: { categoryId: $(this).val() },
            success: function (data) {
                console.log("Success!!!", data);
                location.reload(true);
            },
            error: function (xhr, err) {
                alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
                alert("responseText: " + xhr.responseText);
            }
        });
        return false;
    }

    $('#categoryId').change(ajaxCategoryChange);        
});

//$(function () {
//    var ajaxFormSubmit = function () {
//        var $form = $(this).parents("form");
//        var url = "/api" + $form.attr("action");
        

//        var options = {
//            url: url,
//            type: $form.attr("method"),
//            data: $form.serialize()
//        }

//        $.ajax(options)
//            .done(function (data) {
//                var $target = $("#result");
//                $target.val(data);
//            });

//        return false;
//    }

//    $("#submit").on('click', ajaxFormSubmit);
//});