﻿class Cart {
    clickIncrement(btn) {
        let data = this.getData(btn);
        data.Quantity++;
        this.postQuantity(data);
    }

    clickDecrement(btn) {
        let data = this.getData(btn);
        data.Quantity--;
        this.postQuantity(data);
    }

    getData(element) {
        var itemLine = $(element).parents('[item-id]');
        var itemLine2nd = $('#orderId').html();
        var itemId = $(itemLine).attr('item-id');
        var newQnty = $(itemLine).find('input').val();
   
        return {
            ProductId: itemId,
            OrderId: itemLine2nd,
            Quantity: newQnty,
        };
    }

    postQuantity(data) {
        $.ajax({
            url: '/Order/RefreshSomeValues',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data)
        }).done(function (response) {

            let itemLine = $('[item-id=' + response.updatedOne.productId + ']');
            itemLine.find('input').val(response.updatedOne.quantity);
            itemLine.find('[subtotal]').html((response.updatedOne.subTotal).fixNumber());
            let cart = response.cart;
            $('[numero-itens]').html(cart.totalItens + ' itens');
            $('[total]').html((cart.total).fixNumber());
            debugger;
        });
    }

    updateQuantity(input) {
        let data = this.getData(input);
        this.postQuantity(data);
    }
}

var cart = new Cart();

Number.prototype.fixNumber = function () {
    return this.toFixed(2).replace('.', ',');
}

