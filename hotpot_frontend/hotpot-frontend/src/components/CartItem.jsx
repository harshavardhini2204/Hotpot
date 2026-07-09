import cartService from "../services/cartService";

function CartItem({ item, reloadCart }) {

    const increase = async () => {

        await cartService.updateQuantity(
            item.cartItemId,
            item.quantity + 1
        );

        reloadCart();

    };

    const decrease = async () => {

        if (item.quantity === 1)
            return;

        await cartService.updateQuantity(
            item.cartItemId,
            item.quantity - 1
        );

        reloadCart();

    };

    const remove = async () => {

        await cartService.removeItem(
            item.cartItemId
        );

        reloadCart();

    };

    return (

        <div className="card mb-3 shadow">

            <div className="card-body">

                <div className="row align-items-center">

                    <div className="col-md-4">

                        <h5>

                            {item.menuItem.itemName}

                        </h5>

                    </div>

                    <div className="col-md-2">

                        ₹{item.unitPrice}

                    </div>

                    <div className="col-md-3">

                        <button
                            className="btn btn-secondary"
                            onClick={decrease}
                        >

                            -

                        </button>

                        <span className="mx-3">

                            {item.quantity}

                        </span>

                        <button
                            className="btn btn-secondary"
                            onClick={increase}
                        >

                            +

                        </button>

                    </div>

                    <div className="col-md-2">

                        ₹{item.quantity * item.unitPrice}

                    </div>

                    <div className="col-md-1">

                        <button
                            className="btn btn-danger"
                            onClick={remove}
                        >

                            X

                        </button>

                    </div>

                </div>

            </div>

        </div>

    );

}

export default CartItem;