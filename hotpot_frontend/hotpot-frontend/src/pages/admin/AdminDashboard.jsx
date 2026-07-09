import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

function AdminDashboard() {

    const navigate = useNavigate();

    const [stats, setStats] = useState({
        users: 0,
        restaurants: 0,
        orders: 0,
        revenue: 0
    });

    useEffect(() => {

        // Replace with API calls later

        setStats({
            users: 125,
            restaurants: 42,
            orders: 820,
            revenue: 125000
        });

    }, []);

    return (

        <div className="admin-dashboard">

            <div className="dashboard-banner">

                <h1>Admin Dashboard</h1>

                <p>
                    Manage your HotPot platform efficiently
                </p>

            </div>

            <div className="container py-5">

                {/* Statistics Cards */}

                <div className="row g-4">

                    <div className="col-md-3">

                        <div
                            className="dashboard-card clickable-card"
                            onClick={() => navigate("/admin/users")}
                        >
                            <h2>{stats.users}</h2>
                            <p>Total Users</p>
                        </div>

                    </div>

                    <div className="col-md-3">

                        <div
                            className="dashboard-card clickable-card"
                            onClick={() => navigate("/admin/restaurants")}
                        >
                            <h2>{stats.restaurants}</h2>
                            <p>Restaurants</p>
                        </div>

                    </div>

                    <div className="col-md-3">

                        <div
                            className="dashboard-card clickable-card"
                            onClick={() => navigate("/admin/orders")}
                        >
                            <h2>{stats.orders}</h2>
                            <p>Total Orders</p>
                        </div>

                    </div>

                    <div className="col-md-3">

                        <div
                            className="dashboard-card clickable-card"
                            onClick={() => navigate("/admin/payments")}
                        >
                            <h2>₹{stats.revenue}</h2>
                            <p>Revenue</p>
                        </div>

                    </div>

                </div>

                {/* Activity + Quick Actions */}

                <div className="row mt-5">

                    <div className="col-lg-8">

                        <div className="activity-card">

                            <h3>Recent Activity</h3>

                            <div className="activity-item">
                                🛒 New Order #1023 placed
                            </div>

                            <div className="activity-item">
                                🍕 New Restaurant Added
                            </div>

                            <div className="activity-item">
                                💳 Payment Received ₹897
                            </div>

                            <div className="activity-item">
                                👤 New User Registered
                            </div>

                        </div>

                    </div>

                    <div className="col-lg-4">

                        <div className="quick-actions">

                            <h3>Quick Actions</h3>

                            <button
                                className="admin-btn"
                                onClick={() => navigate("/admin/users")}
                            >
                                👥 Manage Users
                            </button>

                            <button
                                className="admin-btn"
                                onClick={() => navigate("/admin/restaurants")}
                            >
                                🍽 Manage Restaurants
                            </button>

                            <button
                                className="admin-btn"
                                onClick={() => navigate("/admin/categories")}
                            >
                                📂 Manage Categories
                            </button>

                            <button
                                className="admin-btn"
                                onClick={() => navigate("/admin/orders")}
                            >
                                📦 View Orders
                            </button>

                            <button
                                className="admin-btn"
                                onClick={() => navigate("/admin/payments")}
                            >
                                💳 View Payments
                            </button>

                        </div>

                    </div>

                </div>

            </div>

        </div>

    );

}

export default AdminDashboard;