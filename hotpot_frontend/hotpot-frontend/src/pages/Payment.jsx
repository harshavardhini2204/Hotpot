import { useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import paymentService from "../services/paymentService";
import { toast } from "react-toastify";

function Payment() {

    const { orderId } = useParams();

    const navigate = useNavigate();

    const [paymentMethod, setPaymentMethod] = useState("Cash on Delivery");

    const [loading, setLoading] = useState(false);

    const handlePayment = async () => {

    try {

        setLoading(true);

        const payment = {

            orderId: Number(orderId),

            paymentMethod,

        };

        await paymentService.createPayment(payment);

        toast.success("Payment Successful");

        navigate("/orders");

    }

    catch (error) {

        console.error(error);

        toast.error("Payment Failed");

    }

    finally {

        setLoading(false);

    }

};

   return (

<div className="payment-page">

    <div className="payment-banner">

        <h1>💳 Secure Payment</h1>

        <p>
            Complete your payment and confirm your order
        </p>

    </div>

    <div className="container py-5">

        <div className="row justify-content-center">

            <div className="col-lg-7">

                <div className="payment-card">

                    <div className="text-center mb-4">

                        <div className="payment-icons">

                            <span>💳</span>
                            <span>📱</span>
                            <span>🏦</span>

                        </div>

                        <h3>Choose Payment Method</h3>

                        <p className="text-muted">
                            Order ID: #{orderId}
                        </p>

                    </div>

                    <div className="payment-methods">

                        <label className="payment-method">

                            <input
                                type="radio"
                                value="Cash on Delivery"
                                checked={
                                    paymentMethod ===
                                    "Cash on Delivery"
                                }
                                onChange={(e) =>
                                    setPaymentMethod(
                                        e.target.value
                                    )
                                }
                            />

                            🚚 Cash On Delivery

                        </label>

                        <label className="payment-method">

                            <input
                                type="radio"
                                value="UPI"
                                checked={
                                    paymentMethod ===
                                    "UPI"
                                }
                                onChange={(e) =>
                                    setPaymentMethod(
                                        e.target.value
                                    )
                                }
                            />

                            📱 UPI Payment

                        </label>

                        <label className="payment-method">

                            <input
                                type="radio"
                                value="Card"
                                checked={
                                    paymentMethod ===
                                    "Card"
                                }
                                onChange={(e) =>
                                    setPaymentMethod(
                                        e.target.value
                                    )
                                }
                            />

                            💳 Credit / Debit Card

                        </label>

                    </div>

                    <button
                        className="pay-btn mt-4"
                        onClick={handlePayment}
                        disabled={loading}
                    >

                        {
                            loading
                                ? "Processing..."
                                : "Confirm Payment "
                        }

                    </button>

                </div>

            </div>

        </div>

    </div>

</div>

);

}

export default Payment;