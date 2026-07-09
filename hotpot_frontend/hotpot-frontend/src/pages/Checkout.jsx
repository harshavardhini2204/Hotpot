import { useState } from "react";
import { useNavigate } from "react-router-dom";
import orderService from "../services/orderService";
import { toast } from "react-toastify";

function Checkout() {

    const navigate = useNavigate();

    const [shippingAddress, setShippingAddress] = useState("");

    const [notes, setNotes] = useState("Cash on Delivery");

    const [loading, setLoading] = useState(false);

    const handlePlaceOrder = async () => {

        try {

            setLoading(true);

            const userId = Number(localStorage.getItem("userId"));

            const order = {

                userId,
               shippingAddress,
            notes

            };

            const result = await orderService.placeOrder(order);

            toast.success("Order Placed Successfully");
             navigate(`/payment/${result.orderId}`);

        }

        catch (error) {

            console.error(error);

            toast.error("Unable to place order");

        }

        finally {

            setLoading(false);

        }

    };

    return (

<div className="checkout-page">

    <div className="checkout-banner">

        <h1>🍽 Checkout</h1>

        <p>
            Complete your order and enjoy delicious food.
        </p>

    </div>

    <div className="container py-5">

        <div className="row g-4">

            {/* LEFT SIDE */}

            <div className="col-lg-8">

                <div className="checkout-card">

                    <h3>📍 Delivery Address</h3>

                    <textarea
                        className="form-control checkout-input"
                        rows="4"
                        placeholder="Enter your delivery address..."
                        value={shippingAddress}
                        onChange={(e) =>
                            setShippingAddress(e.target.value)
                        }
                    />

                </div>

                <div className="checkout-card mt-4">

                    <h3>📝 Special Instructions</h3>

                    <textarea
                        className="form-control checkout-input"
                        rows="3"
                        placeholder="Less spicy, extra cheese, etc."
                        value={notes}
                        onChange={(e) =>
                            setNotes(e.target.value)
                        }
                    />

                </div>

                

            </div>

            {/* RIGHT SIDE */}

            <div className="col-lg-4">

                <div className="order-summary-card">

                    <h3>🧾 Order Summary</h3>

                    <div className="summary-row">
                        <span>Food Items</span>
                        <span>Added from Cart</span>
                    </div>

                    <div className="summary-row">
                        <span>Delivery Fee</span>
                        <span>₹50</span>
                    </div>

                    <div className="summary-row">
                        <span>Taxes</span>
                        <span>₹100</span>
                    </div>

                    <hr />

                    <div className="summary-total">
                        <span>Total</span>
                        <span>Calculated at Payment</span>
                    </div>

                    <button
                        className="place-order-btn"
                        disabled={loading}
                        onClick={handlePlaceOrder}
                    >

                        {
                            loading
                                ? "Placing Order..."
                                : "Place Order "
                        }

                    </button>

                </div>

            </div>

        </div>

    </div>

</div>

);

}

export default Checkout;