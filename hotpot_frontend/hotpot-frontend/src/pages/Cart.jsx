import { useEffect, useState } from "react";
import cartService from "../services/cartService";
import { Link } from "react-router-dom";

function Cart() {

    const [cart, setCart] = useState(null);
    const [loading, setLoading] = useState(true);

   

    const userId = Number(localStorage.getItem("userId"));

   const loadCart = async () => {

    try {

        const data = await cartService.getCart(userId);

        setCart(data);

    }
    catch (error) {

        console.error(error);

    }
    finally {

        setLoading(false);

    }

};

useEffect(() => {

    loadCart();

}, [userId]);



    if (loading)
        return <h3 className="text-center mt-5">Loading...</h3>;

    if (!cart || cart.cartItems.length === 0)
        return (
            <div className="empty-cart">

    <img
        src="https://cdn-icons-png.flaticon.com/512/2038/2038854.png"
        alt=""
    />

    <h2>Your Cart is Empty</h2>

    <p>
        Add some delicious food to get started.
    </p>

    <Link
        to="/restaurants"
        className="btn btn-primary"
    >
        Browse Restaurants
    </Link>

</div>
        );

    const total = cart.cartItems.reduce(
        (sum, item) => sum + item.quantity * item.unitPrice,
        0
    );
    const tax=100;
    const delivery=50;
    const grandtotal=total+tax+delivery;
const updateItemQuantity = async (
    cartItemId,
    currentQuantity,
    change
) => {

    const newQuantity = currentQuantity + change;

   

    try {

        if (newQuantity <= 0) {

            await cartService.removeItem(cartItemId);

        }
        else {

            await cartService.updateQuantity(
                cartItemId,
                newQuantity
            );

        }

        loadCart();

    }
    catch (error) {

        console.error(error);

    }

};

    return (

        <div className="container py-5">
            

    <div className="cart-header mb-4">
        <h1>🛒 Your Cart</h1>
        <p>Review your delicious selections</p>
    </div>

    <div className="row">

        <div className="col-lg-8">

            {cart.cartItems.map(item => (

                <div
                    key={item.cartItemId}
                    className="cart-item-card mb-4"
                >

                    <img
                        src={item.imageUrl}
                        alt={item.itemName}
                        className="cart-image"
                    />

                    <div className="cart-details">

                        <h4>{item.itemName}</h4>

                        <p>
                            ₹{item.unitPrice}
                        </p>
                        

                        <div className="quantity-section">

    <button
        onClick={() =>
            updateItemQuantity(
                item.cartItemId,
                item.quantity,
                -1
            )
        }
    >
        −
    </button>

    <span>{item.quantity}</span>

    <button
        onClick={() =>
            updateItemQuantity(
                item.cartItemId,
                item.quantity,
                1
            )
        }
    >
        +
    </button>

</div>

                    </div>

                    <div className="cart-total">
                        ₹{item.unitPrice * item.quantity}
                    </div>

                </div>

            ))}

        </div>

        <div className="col-lg-4">

            <div className="order-summary">

                <h3>Order Summary</h3>

                <div className="summary-row">
                    <span>Subtotal</span>
                    <span>₹{total}</span>
                </div>

                <div className="summary-row">
                    <span>Delivery</span>
                    <span>₹50</span>
                </div>

                <div className="summary-row">
                    <span>Tax</span>
                    <span>₹100</span>
                </div>

                <hr />

                <div className="summary-total">
                    <span>Total</span>
                    <span>₹{grandtotal}</span>
                </div>

                <Link
    to="/checkout"
    className="checkout-btn text-decoration-none d-block text-center"
>
    Proceed to Checkout
</Link>

            </div>

        </div>

    </div>

</div>

    );

}

export default Cart;