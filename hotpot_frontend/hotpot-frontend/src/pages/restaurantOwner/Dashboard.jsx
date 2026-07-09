import { Link } from "react-router-dom";

function Dashboard() {

    return (

        <div className="owner-dashboard">

            <div className="owner-banner">

                <h1>🍽 Restaurant Dashboard</h1>

                <p>
                    Manage your restaurant, menu and customer orders
                </p>

            </div>

            <div className="container py-5">

                {/* Stats Row */}

                <div className="row g-4 mb-5">

                    <div className="col-md-3">

                        <div className="owner-stat-card">

                            <h2>125</h2>

                            <p>Total Orders</p>

                        </div>

                    </div>

                    <div className="col-md-3">

                        <div className="owner-stat-card">

                            <h2>₹45K</h2>

                            <p>Revenue</p>

                        </div>

                    </div>

                    <div className="col-md-3">

                        <div className="owner-stat-card">

                            <h2>28</h2>

                            <p>Menu Items</p>

                        </div>

                    </div>

                    <div className="col-md-3">

                        <div className="owner-stat-card">

                            <h2>12</h2>

                            <p>Pending Orders</p>

                        </div>

                    </div>

                </div>

                {/* Quick Actions */}

                <div className="row g-4">

                    <div className="col-lg-4">

                        <div className="owner-card text-center">

                            <div className="owner-icon">
                                🍔
                            </div>

                            <h4>Menu Management</h4>

                            <p>
                                Add, edit and manage menu items.
                            </p>

                            <Link
                                to="/owner/menuitems"
                                className="owner-btn"
                            >
                                Open Menu
                            </Link>

                        </div>

                    </div>

                    <div className="col-lg-4">

                        <div className="owner-card text-center">

                            <div className="owner-icon">
                                📦
                            </div>

                            <h4>Orders</h4>

                            <p>
                                Track incoming and completed orders.
                            </p>

                            <Link
                                to="/owner/orders"
                                className="owner-btn"
                            >
                                View Orders
                            </Link>

                        </div>

                    </div>

                    <div className="col-lg-4">

                        <div className="owner-card text-center">

                            <div className="owner-icon">
                                👤
                            </div>

                            <h4>Restaurant Profile</h4>

                            <p>
                                Update restaurant information.
                            </p>

                            <Link
                                to="/owner/profile"
                                className="owner-btn"
                            >
                                Edit Profile
                            </Link>

                        </div>

                    </div>

                </div>

            </div>

        </div>

    );

}

export default Dashboard;