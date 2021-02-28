var Categories = [];
var $ = jQuery;
var instid;
//fetch categories from database
function LoadFuclty(element) {

   
    instid = element.options[element.selectedIndex].value;
    if (document.getElementById("MainInstitution")) {
    document.getElementById("MainInstitution").disabled = false;
    if (instid == '0') {
        document.getElementById("MainInstitution").disabled = true;
        }
    
    document.getElementById("MainInstitution").value = instid;
    }
        //ajax function for fetch data
        $.ajax({
            type: "GET",
            url: '/Authors/GetFuclty?instid=' + instid,
            //url: '/Author/getInstitutions',
            success: function (data) {
               
                Categories = data;
               
              
                //render catagory
                renderFuclty(element);
            }
        })
    
    //else {
    //    //render catagory to the element
    //    renderCategory();
    //}
}



function renderFuclty() {
 
    var $ele = $('#Faculty');



    $ele.empty();


    $ele.append($('<option/>').val('0').text('Select'));
    $.each(Categories, function (i, val) {
      
        $ele.append($('<option/>').val(val.Facultyid ).text(val.Name));
    })
}
var departments = [];
function LoadDepartment(element) {

    var facid = element.options[element.selectedIndex].value;
   
    //ajax function for fetch data
    $.ajax({
        type: "GET",
        url: '/Authors/GetDepartment?facid=' + facid + '&instid=' + instid,
        //url: '/Author/getInstitutions',
        success: function (data) {
            
            departments = data;
        

            //render catagory
            rendeDepartment();
        }
    })

    //else {
    //    //render catagory to the element
    //    renderCategory();
    //}
}



function rendeDepartment() {
    var $ele = $('#Department');



    $ele.empty();
    $ele.append($('<option/>').val('0').text('Select'));
    $.each(departments, function (i, val) {
        
        $ele.append($('<option/>').val(val.Departmentid).text(val.Name));
    })
}



//fetch products
//function LoadProduct(categoryDD) {
//    var instid = categoryDD.options[categoryDD.selectedIndex].value;
//    $.ajax({
//        type: "GET",
//        url: '/Authors/GetFuclty?instid=' + instid,
       
//        success: function (data) {
           
//            //render products to appropriate dropdown
//            renderProduct($(categoryDD).parents('.mycontainer').find('select.product'), data);
//        },
//        error: function (error) {
//            console.log(error);
//        }
//    })
//}

//function renderProduct(element, data) {
    
//    //render product
//    var $ele = $(element);
//    $ele.empty();
//    $ele.append($('<option/>').val('0').text('Select'));

    
//    for (var i in JSON.stringify(data)) {
//        alert(i);
//    }
   
//}

$(document).ready(function () {
    var $ = jQuery;
    $('#addinstitfac').click(function () {
        //validation and add order items
        var isAllValid = true;
        if ($('#Institutions').val() == "0" || $('#Institutions').val() == null) {
            isAllValid = false;
            $('#Institutions').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#Institutions').siblings('span.error').css('visibility', 'hidden');
        }

        if ($('#Faculty').val() == "0" || $('#Faculty').val() == null) {
            isAllValid = false;
            $('#Faculty').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#Faculty').siblings('span.error').css('visibility', 'hidden');
        }
        //if ($('#Department').val() == "0" || $('#Department').val() == null) {
        //    isAllValid = false;
        //    $('#Department').siblings('span.error').css('visibility', 'visible');
        //}
        //else {
        //    $('#Department').siblings('span.error').css('visibility', 'hidden');
        //}
        //if ($('#Positionjob').val() == "0" || $('#Positionjob').val() == null) {
        //    isAllValid = false;
        //    $('#Positionjob').siblings('span.error').css('visibility', 'visible');
        //}
        //else {
        //    $('#Positionjob').siblings('span.error').css('visibility', 'hidden');
        //}

        //if (!($('#quantity').val().trim() != '' && (parseInt($('#quantity').val()) || 0))) {
        //    isAllValid = false;
        //    $('#quantity').siblings('span.error').css('visibility', 'visible');
        //}
        //else {
        //    $('#quantity').siblings('span.error').css('visibility', 'hidden');
        //}

        //if (!($('#rate').val().trim() != '' && !isNaN($('#rate').val().trim()))) {
        //    isAllValid = false;
        //    $('#rate').siblings('span.error').css('visibility', 'visible');
        //}
        //else {
        //    $('#rate').siblings('span.error').css('visibility', 'hidden');
        //}

        if (isAllValid) {
            var id = $('#Institutions').val() + '_' + $('#Faculty').val() ;

            var ele = document.getElementById(id);
            if (ele) {
                alert('this item already exists');
                return;
            }
            

            var text = ` <tr class="mycontainer"  id=` + id + `>

                <td width="20%">
                    <span class="pc form-group">

                      `            + $('#Institutions option:selected').html() + `

                                        <input type="hidden" name="institutions" value="`+ $('#Institutions').val() + `" />

                    </span>
                </td>
                
                <td class="product form-group" width="25%">
                    <span class="product form-group">
                        `+ $('#Faculty option:selected').html() + `
                                        <input type="hidden" name="Faculty" value=" `+ $('#Faculty').val() + `" />
                    </span>
                </td>

                
                <td width="20%">
                    <input type="button" id="remove" value="Remove" style="width:80px" class="remove btn btn-danger" />
                </td>
            </tr>`

                ;

            //$('.pc', $newRow).val($('#Institutions').val());
            //$('.mi', $newRow).val($('#MainInstitution').val());
            //$('.product', $newRow).val($('#Faculty').val());

            //$('.dep', $newRow).val($('#Department').val());
            //$('.pos', $newRow).val($('#Positionjob').val());



            //Replace add button with remove button
            //$('#add', $newRow).addClass('remove').val('Remove').removeClass('btn-success').addClass('btn-danger');

            ////remove id attribute from new clone row
            //$('#Institutions,#Faculty', $newRow).removeAttr('id');
            //$('span.error', $newRow).remove();

            ////append clone row
            $('#AuthposItems').append(text);

            //clear select data
            $('#Institutions,#Faculty').val('0');
           
            document.getElementById("btnsubmit").disabled = false;
            //$('#quantity,#rate').val('');
            $('#orderItemError').empty();
        }

    })
    //Add button click event
    $('#addinstit').click(function () {
        //validation and add order items
        var isAllValid = true;
        if ($('#Institutions').val() == "0" || $('#Institutions').val() == null) {
            isAllValid = false;
            $('#Institutions').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#Institutions').siblings('span.error').css('visibility', 'hidden');
        }

        if ($('#Faculty').val() == "0" || $('#Faculty').val() == null) {
            isAllValid = false;
            $('#Faculty').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#Faculty').siblings('span.error').css('visibility', 'hidden');
        }
        if ($('#Department').val() == "0"||$('#Department').val() == null) {
            isAllValid = false;
            $('#Department').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#Department').siblings('span.error').css('visibility', 'hidden');
        }
        if ($('#Positionjob').val() == "0" || $('#Positionjob').val() == null) {
            isAllValid = false;
            $('#Positionjob').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#Positionjob').siblings('span.error').css('visibility', 'hidden');
        }

        //if (!($('#quantity').val().trim() != '' && (parseInt($('#quantity').val()) || 0))) {
        //    isAllValid = false;
        //    $('#quantity').siblings('span.error').css('visibility', 'visible');
        //}
        //else {
        //    $('#quantity').siblings('span.error').css('visibility', 'hidden');
        //}

        //if (!($('#rate').val().trim() != '' && !isNaN($('#rate').val().trim()))) {
        //    isAllValid = false;
        //    $('#rate').siblings('span.error').css('visibility', 'visible');
        //}
        //else {
        //    $('#rate').siblings('span.error').css('visibility', 'hidden');
        //}

        if (isAllValid) {
            var id = $('#Institutions').val() +  '_' + $('#Faculty').val() + '_' + $('#Department').val() + '_' + $('#Positionjob').val();

            var ele = document.getElementById(id);
            if (ele) {
                alert('this item already exists');
                return;
            }
            var isChecked = $('#MainInstitution').prop('checked');
            var values;
            if (isChecked == true) {
                values = "checked";
            }
            if (isChecked == false) {
                values = "";
            }
           
            if ($('#AuthposItems tr').length==0) {
                values = "checked"
            }
            var $newRow = $('#mainrow').clone().attr("id", id);
           
          var text=  ` <tr class="mycontainer"  id=`+id+`>

                <td width="20%">
                    <span class="pc form-group">

                      `            + $('#Institutions option:selected').html() +`

                                        <input type="hidden" name="institutions" value="`+ $('#Institutions').val() +`" />

                    </span>
                </td>
                <td>

                        <input type="radio" class="mi radio" name="MainInstitute" id="MainInstitution" `+ values + ` required value="` + $('#Institutions').val() +`" />
                    
                </td>
                <td class="product form-group" width="25%">
                    <span class="product form-group">
                        `+ $('#Faculty option:selected').html() +`
                                        <input type="hidden" name="Faculty" value=" `+ $('#Faculty').val() +`" />
                    </span>
                </td>

                <td width="20%">
                    <span class="dep form-group">
                        `+ $('#Department option:selected').html() +`
                                        <input type="hidden" name="Department" value=" `+ $('#Department').val() +`" />

                    </span>
                </td>

                <td width="20%">
                    <span class="product form-group">
                         `+ $('#Positionjob option:selected').html() +`
                                        <input type="hidden" name="positionjob" value="`+ $('#Positionjob').val() +`" />
                    </span>
                </td>
                <td width="20%">
                    <input type="button" id="remove" value="Remove" style="width:80px" class="remove btn btn-danger" />
                </td>
            </tr>`

          ;

            $('.pc', $newRow).val($('#Institutions').val());
            $('.mi', $newRow).val($('#MainInstitution').val());
            $('.product', $newRow).val($('#Faculty').val());

            $('.dep', $newRow).val($('#Department').val());
            $('.pos', $newRow).val($('#Positionjob').val());



            //Replace add button with remove button
            $('#add', $newRow).addClass('remove').val('Remove').removeClass('btn-success').addClass('btn-danger');

            //remove id attribute from new clone row
            $('#Institutions,#MainInstitution,#Faculty,#Department,#Positionjob,#add', $newRow).removeAttr('id');
            $('span.error', $newRow).remove();
           
            //append clone row
            $('#AuthposItems').append(text);

            //clear select data
            $('#Institutions,#Faculty,#Department,#Positionjob').val('0');
            document.getElementById("MainInstitution").disabled = true;
            document.getElementById("btnsubmit").disabled = false;
            //$('#quantity,#rate').val('');
            $('#orderItemError').empty();
        }

    })
    
    $('#addResarchFields').click(function () {
        var id = $('#newarResarchFields').val().trim();
        var ele = document.getElementById(id);
        if (ele) {
            alert("Already Exists");
            
        }
        else {

        var text = ` <tr class="researchcontainer" id=` + $('#newarResarchFields').val().trim() + `>

                <td width="20%">
                    <span class="form-group">

                      `            + $('#newarResarchFields').val() + `

                                        <input type="hidden" name="newarResarchFields" value="`+ $('#newarResarchFields').val().trim() + `" />

                    </span>
                </td>
              
                <td class=" form-group" width="25%">
                    <span class="form-group">
                        `+ $('#newenResarchFields').val() + `
                                        <input type="hidden" name="newenResarchFields" value=" `+ $('#newenResarchFields').val() + `" />
                    </span>
                </td>

                <td width="20%">
                    <input type="button" id="remove" value="Remove" style="width:80px" class="remove btn btn-danger" />
                </td>
            </tr>`

            ;
            if (isEmptyOrSpaces($('#newarResarchFields').val())) {

            $('#newarResarchFields').focus();
              }
            //if (isEmptyOrSpaces($('#newenResarchFields').val())) {
          
            //    $('#newenResarchFields').focus();
            //}
         
        if (!isEmptyOrSpaces($('#newarResarchFields').val()))  {
            $('#researchdetails').append(text);
            $('#newenResarchFields').val('');
            $('#newarResarchFields').val('');
            }
        }
     
    })

    function isEmptyOrSpaces(str) {
        return str === null || str.match(/^ *$/) !== null || str.length==0;
    }

    $('#researchdetails').on('click', '.remove', function () {
        $('#newenResarchFields').val('');
        $('#newarResarchFields').val('');

        $(this).parents('tr').remove();
    });


    //remove button click event
    $('#AuthposItems').on('click', '.remove', function () {
 
        var radios = document.getElementsByName('MainInstitute');
        var exist = false;
        for (var i = 0, length = radios.length; i < length; i++) {
            if (radios[i].checked) {
                exist = true;
                // do whatever you want with the checked radio
               

                // only one radio can be logically checked, don't check the rest
                break;
            }
        }
      

     
        if (exist == false) {

            document.getElementById("btnsubmit").disabled = true;
        }
        

        if (document.getElementsByName("mycontainer").length == 1) {

            document.getElementById("btnsubmit").disabled = true;
        }
        $(this).parents('tr').remove();
        if ($('#AuthposItems tr').length == 1) {

            document.getElementsByName('MainInstitute')[1].checked = true;

        }
        if ($('#AuthposItems tr').length == 0) {
            
            document.getElementById("btnsubmit").disabled = true;

        }

    });

    $('#submit').click(function () {
        var isAllValid = true;

        //validate order items
        $('#orderItemError').text('');
        var list = [];
        var errorItemCount = 0;
        $('#AuthposItems tbody tr').each(function (index, ele) {
            if (
                $('select.product', this).val() == "0" ||
                (parseInt($('.quantity', this).val()) || 0) == 0 ||
                $('.rate', this).val() == "" ||
                isNaN($('.rate', this).val())
                ) {
                errorItemCount++;
                $(this).addClass('error');
            } else {
                var orderItem = {
                    ProductID: $('select.product', this).val(),
                    Quantity: parseInt($('.quantity', this).val()),
                    Rate: parseFloat($('.rate', this).val())
                }
                list.push(orderItem);
            }
        })

        if (errorItemCount > 0) {
            $('#orderItemError').text(errorItemCount + " invalid entry in order item list.");
            isAllValid = false;
        }

        if (list.length == 0) {
            $('#orderItemError').text('At least 1 order item required.');
            isAllValid = false;
        }

        if ($('#orderNo').val().trim() == '') {
            $('#orderNo').siblings('span.error').css('visibility', 'visible');
            isAllValid = false;
        }
        else {
            $('#orderNo').siblings('span.error').css('visibility', 'hidden');
        }

        if ($('#orderDate').val().trim() == '') {
            $('#orderDate').siblings('span.error').css('visibility', 'visible');
            isAllValid = false;
        }
        else {
            $('#orderDate').siblings('span.error').css('visibility', 'hidden');
        }

        if (isAllValid) {
            var data = {
                OrderNo: $('#orderNo').val().trim(),
                OrderDateString: $('#orderDate').val().trim(),
                Description: $('#description').val().trim(),
                OrderDetails: list
            }

            $(this).val('Please wait...');

            $.ajax({
                type: 'POST',
                url: '/home/save',
                data: JSON.stringify(data),
                contentType: 'application/json',
                success: function (data) {
                    if (data.status) {
                        alert('Successfully saved');
                        //here we will clear the form
                        list = [];
                        $('#orderNo,#orderDate,#description').val('');
                        $('#AuthposItems').empty();
                    }
                    else {
                        alert('Error');
                    }
                    $('#submit').text('Save');
                },
                error: function (error) {
                    console.log(error);
                    $('#submit').text('Save');
                }
            });
        }

    });

});

//LoadCategory($('#Institutions'));
