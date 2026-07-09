import { useEffect, useState } from "react";
import orderService from "../services/orderService";
import {  useNavigate } from "react-router-dom";

function Orders() {
    const navigate=useNavigate();

    const [orders, setOrders] = useState([]);

    const [loading, setLoading] = useState(true);
    

     useEffect(() => {

    const fetchOrders = async () => {

        try {

            const data = await orderService.getMyOrder();

            setOrders(data);

        }
        catch (error) {

            console.error(error);

        }
        finally {

            setLoading(false);

        }

    };

    fetchOrders();

}, []);

    

    if (loading)

        return <h3 className="text-center mt-5">Loading Orders...</h3>;

   return (
    <div className="container py-5">

        <div className="cart-header mb-5">
            <h1>📦 My Orders</h1>
            <p>Track and manage your food orders</p>
        </div>

        <div className="table-modern">

            <table className="table mb-0">

                <thead>
                    <tr>
                        <th>Order ID</th>
                        <th>Date</th>
                        <th>Status</th>
                        <th>Total</th>
                        <th></th>
                    </tr>
                </thead>

                <tbody>

                    {orders.map(order => (

                        <tr key={order.orderId}>

                            <td>#{order.orderId}</td>

                            <td>
                                {new Date(
                                    order.orderDate
                                ).toLocaleString()}
                            </td>

                            <td>

                                <span
                                    className={`badge ${
                                        order.status === "Delivered"
                                            ? "bg-success"
                                            : order.status === "Pending"
                                            ? "bg-warning text-dark"
                                            : "bg-primary"
                                    }`}
                                >
                                    {order.orderStatus}
                                </span>

                            </td>

                            <td>
                                ₹{order.totalAmount}
                            </td>

                            <td>

                                <button
                                    className="btn-modern"
                                    onClick={() =>
                                        navigate(
                                            `/orders/${order.orderId}`
                                        )
                                    }
                                >
                                    View Details
                                </button>

                            </td>

                        </tr>

                    ))}

                </tbody>

            </table>

        </div>

    </div>
);

}
export default Orders;
