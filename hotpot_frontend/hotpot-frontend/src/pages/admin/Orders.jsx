import { useEffect, useState } from "react";
import orderService from "../../services/orderService";
import { toast } from "react-toastify";

function Orders() {

    const [orders, setOrders] = useState([]);
    const [search, setSearch] = useState("");

    const loadOrders = async () => {

        try {

            const data = await orderService.getAllOrders();

            setOrders(data);

        }

        catch (error) {

            console.error(error);

        }

    };

    useEffect(() => {

        loadOrders();

    }, []);

    const updateStatus = async (id, status) => {

        try {

            await orderService.updateOrderStatus(id, status);

            toast.success("Order Updated");

            loadOrders();

        }

        catch (error) {

            console.error(error);

            toast.error("Update Failed");

        }

    };

    const filteredOrders = orders.filter(order =>
        order.orderId.toString().includes(search) ||
        (order.userName || "")
            .toLowerCase()
            .includes(search.toLowerCase())
    );

    const getStatusBadge = (status) => {

        switch(status){

            case "Delivered":
                return "bg-success";

            case "Preparing":
                return "bg-warning text-dark";

            case "OutForDelivery":
                return "bg-info";

            case "Cancelled":
                return "bg-danger";

            default:
                return "bg-secondary";
        }
    };

    return (

        <div className="container py-4">

            <div className="admin-header mb-4">

                <h2>📦 Order Management</h2>

                <p>
                    Track and manage customer orders
                </p>

            </div>

            <div className="card shadow-sm border-0 rounded-4">

                <div className="card-body">

                    <div className="d-flex justify-content-between align-items-center mb-4">

                        <h4 className="fw-bold mb-0">
                            Orders
                        </h4>

                        <input
                            type="text"
                            className="form-control order-search"
                            placeholder="Search Order ID / Customer"
                            value={search}
                            onChange={(e) =>
                                setSearch(e.target.value)
                            }
                        />

                    </div>

                    <div className="table-responsive">

                        <table className="table align-middle">

                            <thead className="table-light">

                                <tr>

                                    <th>ID</th>
                                    <th>Customer</th>
                                    <th>Total</th>
                                    <th>Status</th>
                                    <th>Payment</th>
                                    <th>Update</th>

                                </tr>

                            </thead>

                            <tbody>

                                {filteredOrders.map(order => (

                                    <tr key={order.orderId}>

                                        <td>
                                            #{order.orderId}
                                        </td>

                                        <td>
                                            {order.userName}
                                        </td>

                                        <td>
                                            ₹{order.totalAmount}
                                        </td>

                                        <td>

                                            <span
                                                className={`badge ${getStatusBadge(order.orderStatus)}`}
                                            >
                                                {order.orderStatus}
                                            </span>

                                        </td>

                                        <td>

                                            <span
                                                className={`badge ${
                                                    order.paymentStatus === "Completed"
                                                        ? "bg-success"
                                                        : "bg-danger"
                                                }`}
                                            >
                                                {order.paymentStatus}
                                            </span>

                                        </td>

                                        <td>

                                            <select
                                                className="form-select form-select-sm status-select"
                                                value={order.orderStatus}
                                                onChange={(e) =>
                                                    updateStatus(
                                                        order.orderId,
                                                        e.target.value
                                                    )
                                                }
                                            >

                                                <option>Pending</option>
                                                <option>Preparing</option>
                                                <option>OutForDelivery</option>
                                                <option>Delivered</option>
                                                <option>Cancelled</option>

                                            </select>

                                        </td>

                                    </tr>

                                ))}

                            </tbody>

                        </table>

                    </div>

                </div>

            </div>

        </div>

    );
}

export default Orders;