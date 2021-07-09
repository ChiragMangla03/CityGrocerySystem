function AddNewCategory() {
    $('#ViewCategories').hide();
    $('#AddNewCategory').show();
}

function Addcategory() {
    var categoryName = $('#CategoryName').val();
    $.ajax({
        type: "POST",
        url: "/Admin/SaveCategory?category=" + categoryName,
        contentType: "application/json; charset=utf-8",
        data: {},
        dataType: "json",
        success: function (data) {
            if (data == 1) {
                //$('#myModal').show();
                //$('#popup_title').val('SUCCESS!');
                //$('#popup_msg').val('Category Added Successfully;');
                alert('Category Added Successfully');
            }
            else if (data == 2) {
                alert('Category Already Added');
            }
            else {
                alert('Some Error Occured, Please contect to Administrator');
            }
        },
        error: function (data) {
            console.log('erroroccured');
            alert('Some Error Occured, Please contect to Administrator');
        }
    });
}

function AddNewProduct() {
    $('#divViewProduct').hide();
    $('#divAddProduct').show();
}

function Cancel() {
    location.reload(true);
}

function SaveNewProduct() {
    var product = {};
    $("#AddProduct TBODY TR").each(function () {
        product.categoryId = $(this).find("td:eq(0) option:selected").val();
        product.Product_name = $(this).find("td:eq(1) input[type='text']").val();
        product.price = $(this).find("td:eq(2) input[type='text']").val();
    });
    $.ajax({
        type: "POST",
        url: '/Admin/SaveProduct',
        data: JSON.stringify(product),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data == 1) {
                alert('Product Added Successfully');
                Cancel();
            }
            else if (data == 2) {
                alert('Product Already Added');
            }
            else {
                alert('Some Error Occured, Please contect to Administrator');
            }
        },
        error: function (data) {
            console.log('erroroccured');
            alert('Some Error Occured, Please contect to Administrator');
        }
    });
}

function UpdateProduct(product_id, Product_name,  price, total_stock) {
    $('#divViewProduct').hide();
    $('#divEditProduct').show();
    $("#UpdateProductDetail > tbody").empty();
    var productbdy = `"
                        <tr>
                            <td>
                                ${Product_name}
                            </td>
                            <td>
                                <input type="text" value="${price}" />
                            </td>
                            <td>
                                ${total_stock}
                            </td>
                            <td>
                                <input type="text" />
                                <input type="hidden" value="${product_id}"/>
                            </td>
                        </tr>
                        "`;
    $('#UpdateProductDetail tbody').append(productbdy);
}

function UpdateProductDetail() {
    var product = {};
    $("#UpdateProductDetail TBODY TR").each(function () {
    
        product.InStock = $(this).find("td:eq(3) input[type='text']").val();
        product.product_id = $(this).find("td:eq(3) input[type='hidden']").val();
        product.price = $(this).find("td:eq(1) input[type='text']").val();
        });
    $.ajax({
        type: "POST",
        url: '/Admin/EditUpdateProduct',
        data: JSON.stringify(product),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data == 1) {
                alert('Product Updated Successfully');
                Cancel();
            }
            else {
                alert('Some Error Occured, Please contect to Administrator');
            }
        },
        error: function (data) {
            console.log('erroroccured');
            alert('Some Error Occured, Please contect to Administrator');
        }
    });
}