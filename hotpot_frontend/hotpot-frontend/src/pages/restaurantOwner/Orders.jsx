import { useEffect, useState } from "react";
import orderService from "../../services/orderService";
import { toast } from "react-toastify";

function Orders() {

    const restaurantId = Number(localStorage.getItem("restaurantId"));

    const [orders, setOrders] = useState([]);
    const [loading, setLoading] = useState(true);
    const [search, setSearch] = useState("");

    useEffect(() => {

        const fetchOrders = async () => {

            try {

                const data =
                    await orderService.getRestaurantOrders(
                        restaurantId
                    );

                setOrders(data);

            }

            catch (error) {

                console.error(error);

                toast.error("Unable to load orders");

            }

            finally {

                setLoading(false);

            }

        };

        fetchOrders();

    }, [restaurantId]);

    const updateStatus = async (
        orderId,
        status
    ) => {

        try {

            await orderService.updateOrderStatus(
                orderId,
                status
            );

            toast.success(
                "Order Status Updated"
            );

            setOrders(
                orders.map(order =>
                    order.orderId === orderId
                        ? {
                              ...order,
                              orderStatus: status
                          }
                        : order
                )
            );

        }

        catch (error) {

            console.error(error);

            toast.error(
                "Unable to update order"
            );

        }

    };

    const filteredOrders =
        orders.filter(order =>
            order.orderId
                .toString()
                .includes(search)
        );

    const pending =
    orders.filter(
        o => o.orderStatus === "Pending"
    ).length;

const preparing =
    orders.filter(
        o => o.orderStatus === "Preparing"
    ).length;

const delivered =
    orders.filter(
        o => o.orderStatus === "Delivered"
    ).length;

    if (loading) {

        return (
            <h3 className="text-center mt-5">
                Loading Orders...
            </h3>
        );

    }

    return (

        <div className="owner-orders-page">

            <div className="owner-orders-banner">

                <h1>Incoming Orders</h1>

                <p>
                    Manage and track customer orders
                </p>

            </div>

            <div className="container py-4">

                <div className="row g-3 mb-4">

                    <div className="col-md-4">

                        <div className="dashboard-card text-center">

                            <h2>{pending}</h2>

                            <p>Pending Orders</p>

                        </div>

                    </div>

                    <div className="col-md-4">

                        <div className="dashboard-card text-center">

                            <h2>{preparing}</h2>

                            <p>Preparing</p>

                        </div>

                    </div>

                    <div className="col-md-4">

                        <div className="dashboard-card text-center">

                            <h2>{delivered}</h2>

                            <p>Delivered</p>

                        </div>

                    </div>

                </div>

                <div className="orders-card">

                    <div className="d-flex justify-content-between align-items-center mb-4">

                        <h3 className="mb-0">
                            Order List
                        </h3>

                        <input
                            type="text"
                            className="form-control orders-search"
                            placeholder="Search Order ID..."
                            value={search}
                            onChange={(e) =>
                                setSearch(
                                    e.target.value
                                )
                            }
                        />

                    </div>

                    <div className="table-responsive">

                        <table className="table owner-table align-middle">

                            <thead>

                                <tr>

                                    <th>Order ID</th>

                                    <th>Date</th>

                                    <th>Total</th>

                                    <th>Status</th>

                                    <th>Update Status</th>

                                </tr>

                            </thead>

                            <tbody>

                                {filteredOrders.map(order => (

                                    <tr key={order.orderId}>

                                        <td>
                                            #{order.orderId}
                                        </td>

                                        <td>
                                            {
                                                new Date(
                                                    order.orderDate
                                                ).toLocaleString()
                                            }
                                        </td>

                                        <td>
                                            ₹{order.totalAmount}
                                        </td>

                                        <td>

                                            <span
                                                className={`status-badge
                                                ${
                                                    order.orderStatus === 5
                                                        ? "delivered"
                                                        : order.orderStatus === 6
                                                        ? "cancelled"
                                                        : order.orderStatus === 3
                                                        ? "preparing"
                                                        : "pending"
                                                }`}
                                            >

                                                {order.orderStatus}

                                            </span>

                                        </td>

                                        <td>

                                            <select
                                                className="form-select status-select"
                                                value={order.orderStatus}
                                                onChange={(e) =>
                                                    updateStatus(
                                                        order.orderId,
                                                        Number(
                                                            e.target.value
                                                        )
                                                    )
                                                }
                                            >

                                                <option value={1}>
                                                    Pending
                                                </option>

                                                <option value={2}>
                                                    Confirmed
                                                </option>

                                                <option value={3}>
                                                    Preparing
                                                </option>

                                                <option value={4}>
                                                    Out For Delivery
                                                </option>

                                                <option value={5}>
                                                    Delivered
                                                </option>

                                                <option value={6}>
                                                    Cancelled
                                                </option>

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