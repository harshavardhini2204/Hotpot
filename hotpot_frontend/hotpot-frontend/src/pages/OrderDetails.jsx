import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import orderService from "../services/orderService";

function OrderDetails() {

    const { id } = useParams();

    const [order, setOrder] = useState(null);

    useEffect(() => {

        const loadOrder = async () => {

            try {

                const data =
                    await orderService.getOrderById(id);

                setOrder(data);

            }
            catch (error) {

                console.error(error);

            }

        };

        loadOrder();

    }, [id]);

    if (!order)
        return (
            <h3 className="text-center mt-5">
                Loading...
            </h3>
        );
        console.log(JSON.stringify(order, null, 2));

    return (

        <div className="container py-5">

            <div className="order-details-header">

                <h1>📦 Order #{order.orderId}</h1>

                <p>
                    Placed on{" "}
                    {new Date(
                        order.orderDate
                    ).toLocaleString()}
                </p>

            </div>

            <div className="row g-4">

                {/* LEFT */}

                <div className="col-lg-8">

                    <div className="details-card">

                        <h3>Order Items</h3>

                        {order.orderItems?.map(item => (

                            <div
                                key={item.orderItemId}
                                className="order-item-row"
                            >

                                <div>

                                    <h5>
                                        {item.menuItem?.itemName}
                                    </h5>

                                    <p>
                                        Qty:
                                        {" "}
                                        {item.quantity}
                                    </p>
                                     <p>
                Price: ₹{item.unitPrice}
            </p>

                                </div>

                                <div>

                                    ₹
                                      ₹{item.quantity * item.unitPrice}

                                </div>

                            </div>

                        ))}

                    </div>

                    <div className="details-card mt-4">

                        <h3>Delivery Address</h3>

                        <p>
                            {order.shippingAddress}
                        </p>

                    </div>
    
<div className="payment-method">
    Method:
    {order.payments?.[0]?.paymentMethod}
</div>
                <div className="payment-status">
    Payment Status:
    <span className="badge bg-success ms-2">
        {order.payments?.[0]?.paymentStatus}
    </span>
</div>

                </div>

                {/* RIGHT */}

                <div className="col-lg-4">

                    <div className="details-card">

                        <h3>Order Summary</h3>

                        <div className="summary-row">
                            <span>Total</span>
                            <strong>
                                ₹{order.totalAmount}
                            </strong>
                        </div>

                        <hr />

                        <div className="summary-row">

                            <span>Status</span>

                            <span
                                className={`status-badge
                                ${
                                    order.status === "Delivered"
                                        ? "success"
                                        : order.status === "Pending"
                                        ? "warning"
                                        : "info"
                                }`}
                            >
                                {order.orderStatus}
                            </span>

                        </div>

                    </div>

                    <div className="details-card mt-4">

                        <h3>Order Tracking</h3>

                        <div className="tracking">

                            <div className="track-step active">
                                ✓ Order Confirmed
                            </div>

                            <div className="track-step active">
                                ✓ Preparing
                            </div>

                            <div
                                className={
                                    order.orderStatus ===
                                        "OutForDelivery" ||
                                    order.orderStatus ===
                                        "Delivered"
                                        ? "track-step active"
                                        : "track-step"
                                }
                            >
                                🚚 Out For Delivery
                            </div>

                            <div
                                className={
                                    order.orderStatus ===
                                    "Delivered"
                                        ? "track-step active"
                                        : "track-step"
                                }
                            >
                                🎉 Delivered
                            </div>

                        </div>

                    </div>

                </div>

            </div>

        </div>

    );

}

export default OrderDetails;