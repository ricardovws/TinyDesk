class Cart {
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
            Quantity: newQnty
        };
    }

    postQuantity(data) {
        $.ajax({
            url: '/Order/UpdateQuantity',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data)
        });
    }

    updateQuantity(input) {
        let data = this.getData(input);
        this.postQuantity(data);
    }
}

var cart = new Cart();

