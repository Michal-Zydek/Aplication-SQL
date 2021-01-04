//Data and Time
$(function () {
    $('.datetimepicker').datetimepicker({
        format: "DD.MM.YYYY hh:mm:ss"
    });
    
});

//Message Succes
window.setTimeout(function () {
    $(".alert-success").fadeTo(500, 0).slideUp(500, function () {
        $(this).remove();
    });
}, 3000);

//Message Alert
window.setTimeout(function () {
    $(".alert-danger").fadeTo(500, 0).slideUp(500, function () {
        $(this).remove();
    });
}, 3000);


//Add new Pytanie
$(document).ready(function () {

    //1. Add new row
    $("#addNew").click(function (e) {
        e.preventDefault();
        var $tableBody = $("#dataTable");
        var $trLast = $tableBody.find("tr:last");
        var $trNew = $trLast.clone();



        var suffix = $trNew.find(':input:first').attr('name').match(/\d+/);
        $.each($trNew.find(':input'), function (i, val) {

            // Replaced Name
            var oldN = $(this).attr('name');
            var newN = oldN.replace('[' + suffix + ']', '[' + (parseInt(suffix) + 1) + ']');
            $(this).attr('name', newN);

            //Replaced value

            var type = $(this).attr('type');
            if (type.toLowerCase() == "text") {
                $(this).attr('value', '');

            }



            // If you have another Type then replace with default value
            $(this).removeClass("input-validation-error");
        });
        $trLast.after($trNew);


        // Re-assign Validation 
        var form = $("form")
            .removeData("validator")
            .removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse(form);
    });





    });

  //Delete Pytanie
$(document).ready(function () {
    $("#remove").click(function (e) {
        e.preventDefault();
        var $tableBody = $("#dataTable");
        var $trLast = $tableBody.find("tr:last");
        var $trNew = $trLast.remove();
    });

});



//Query UserSQLController
$(document).ready(function () {
    $("#play").click(function (e) {
        var asks = $("#ask").val();
        $.ajax({
            url: '/UserSQL/Query_sys',
            type: 'POST',
            dataType: 'json',
            data: { query: asks },
            success: function (Table) {        
                
                $('#table').html(Table);  

            },
            error: function (data) {
                $('#message').html(data);
               
            }

        });
    });

});



//Delete Test 
function DeleteT(ID) {
    var ans = confirm("Czy chcesz usunąć test ?");

    if (ans) {
        $.ajax({
            url: "/DeleteTest/Delete",
            data:{ ID_Test: ID },
            type: "POST",
            dataType: "json",
            success: function (data) {

                window.location.href =data.Url;
            },
            error: function (errormessage) {

                alert(errormessage.responseText);
            }
        });
    }
}

//Delete Baza
function DeleteB(ID) {
    var ans = confirm("Usuwając baze usuniesz też wszystkie testy z nią połączoną.Czy chcesz usunąć Baze ?");

    if (ans) {
        $.ajax({
            url: "/DeleteBaza/Delete",
            data: { ID_Baza: ID },
            type: "POST",
            dataType: "json",
            success: function (data) {

                window.location.href = data.Url;
            },
            error: function (errormessage) {

                alert(errormessage.responseText);
            }
        });
    }
}


function EditT(ID) {
        $.ajax({
            url: "/EditTest/Check_Odp",
            data: { Id_Test: ID },
            type: "POST",
            dataType: "json",
            success: function (data) {


                if (data == 0) {
                    alert("Test zawiera odpowiedzi nie wolno edytować");
                }
                else {

                    window.location.href = data.Url;
                }
                
                
            },
            error: function (errormessage) {

                alert(errormessage.responseText);
            }
        });
    
}

