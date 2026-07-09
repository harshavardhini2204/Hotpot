import { BrowserRouter, Route, Routes, useLocation } from "react-router-dom";
import { Navbar } from "./components/Navbar";
import "./App.css";

import LandingPage from "./pages/LandingPage";
import Home from "./pages/Home";
import Login from "./pages/Login";
import Register from "./pages/Register";

import Restaurants from "./pages/Restaurants";
import RestaurantDetails from "./pages/RestaurantDetails";
import { MenuItemDetails } from "./pages/MenuItemDetails";

import Cart from "./pages/Cart";
import Orders from "./pages/Orders";
import OrderDetails from "./pages/OrderDetails";
import Checkout from "./pages/Checkout";
import Payment from "./pages/Payment";
import Profile from "./pages/Profile";

import AdminDashboard from "./pages/admin/AdminDashboard";
import Categories from "./pages/admin/Categories";
import AdminOrders from "./pages/admin/Orders";
import RestaurantsAdmin from "./pages/admin/Restaurants";
import Users from "./pages/admin/Users";
import Payments from "./pages/admin/Payments";

import Dashboard from "./pages/restaurantOwner/Dashboard";
import MenuManagement from "./pages/restaurantOwner/MenuManagement";
import AddMenuItem from "./pages/restaurantOwner/AddMenuItem";
import EditMenuItem from "./pages/restaurantOwner/EditMenuItem";
import OwnerOrders from "./pages/restaurantOwner/Orders";
import OwnerProfile from "./pages/restaurantOwner/Profile";

import ProtectedRoute from "./routes/ProtectedRoute";

function AppLayout() {
    const location = useLocation();

    const hideNavbar =
        location.pathname === "/";

    return (
        <>
            {!hideNavbar && <Navbar />}

            <Routes>

                {/* Landing Page */}
                <Route
                    path="/"
                    element={<LandingPage />}
                />

                {/* Auth */}
                <Route
                    path="/login"
                    element={<Login />}
                />

                <Route
                    path="/register"
                    element={<Register />}
                />

                {/* Customer */}
                <Route
                    path="/home"
                    element={
                        <ProtectedRoute>
                            <Home />
                        </ProtectedRoute>
                    }
                />

                <Route
                    path="/restaurants"
                    element={<Restaurants />}
                />

                <Route
                    path="/restaurants/:id"
                    element={<RestaurantDetails />}
                />

                <Route
                    path="/menu/:id"
                    element={<MenuItemDetails />}
                />

                <Route
                    path="/cart"
                    element={
                        <ProtectedRoute>
                            <Cart />
                        </ProtectedRoute>
                    }
                />

                <Route
                    path="/orders"
                    element={
                        <ProtectedRoute>
                            <Orders />
                        </ProtectedRoute>
                    }
                />

                <Route
                    path="/orders/:id"
                    element={<OrderDetails />}
                />

                <Route
                    path="/checkout"
                    element={
                        <ProtectedRoute>
                            <Checkout />
                        </ProtectedRoute>
                    }
                />

                <Route
                    path="/payment"
                    element={
                        <ProtectedRoute>
                            <Payment />
                        </ProtectedRoute>
                    }
                />

                <Route
                    path="/payment/:orderId"
                    element={
                        <ProtectedRoute>
                            <Payment />
                        </ProtectedRoute>
                    }
                />

                <Route
                    path="/profile"
                    element={
                        <ProtectedRoute>
                            <Profile />
                        </ProtectedRoute>
                    }
                />

                {/* Admin */}
                <Route
                    path="/admin/dashboard"
                    element={
                        <ProtectedRoute>
                            <AdminDashboard />
                        </ProtectedRoute>
                    }
                />

                <Route
                    path="/admin/categories"
                    element={
                        <ProtectedRoute>
                            <Categories />
                        </ProtectedRoute>
                    }
                />

                <Route
                    path="/admin/restaurants"
                    element={
                        <ProtectedRoute>
                            <RestaurantsAdmin />
                        </ProtectedRoute>
                    }
                />

                <Route
                    path="/admin/users"
                    element={
                        <ProtectedRoute>
                            <Users />
                        </ProtectedRoute>
                    }
                />

                <Route
                    path="/admin/orders"
                    element={
                        <ProtectedRoute>
                            <AdminOrders />
                        </ProtectedRoute>
                    }
                />

                <Route
                    path="/admin/payments"
                    element={
                        <ProtectedRoute>
                            <Payments />
                        </ProtectedRoute>
                    }
                />

                {/* Restaurant Owner */}
                <Route
                    path="/owner/dashboard"
                    element={
                        <ProtectedRoute>
                            <Dashboard />
                        </ProtectedRoute>
                    }
                />

                <Route
                    path="/owner/menuitems"
                    element={
                        <ProtectedRoute>
                            <MenuManagement />
                        </ProtectedRoute>
                    }
                />

                <Route
                    path="/owner/menuitems/add"
                    element={
                        <ProtectedRoute>
                            <AddMenuItem />
                        </ProtectedRoute>
                    }
                />

                <Route
                    path="/owner/menuitems/edit/:id"
                    element={
                        <ProtectedRoute>
                            <EditMenuItem />
                        </ProtectedRoute>
                    }
                />

                <Route
                    path="/owner/orders"
                    element={
                        <ProtectedRoute>
                            <OwnerOrders />
                        </ProtectedRoute>
                    }
                />

                <Route
                    path="/owner/profile"
                    element={
                        <ProtectedRoute>
                            <OwnerProfile />
                        </ProtectedRoute>
                    }
                />

            </Routes>
        </>
    );
}

function App() {
    return (
        <BrowserRouter>
            <AppLayout />
        </BrowserRouter>
    );
}

export default App;