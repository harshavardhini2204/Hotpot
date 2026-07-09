import { useContext, useState } from "react";
import { useNavigate } from "react-router-dom";
import AuthContext from "../context/AuthContext";
import authService from "../services/authService";
import { jwtDecode } from "jwt-decode";
import { toast } from "react-toastify";
import restaurantService from "../services/restaurantService";

 function Login()
{
    const navigate=useNavigate();
    const {setUser}=useContext(AuthContext);
    const[login,setLogin]=useState({
        email:"",
        password:"",
    });
    const handleChange=(event)=>{
        setLogin({
            ...login,
            [event.target.name]:event.target.value
        });
    };
    const handleSubmit=async(event)=>{
        event.preventDefault();
        try{
            const response = await authService.login(login);

localStorage.setItem("token", response.data.token);
localStorage.setItem("userId", response.data.userId);
localStorage.setItem("role", response.data.role);
localStorage.setItem("email", response.data.email);
 if (response.data.restaurantId) {

            localStorage.setItem(
                "restaurantId",
                response.data.restaurantId
            );

        }


const user = jwtDecode(response.data.token);

setUser(user);
            toast.success("Login Successful");
            if (response.data.role === "Admin") {
    navigate("/admin/dashboard");
}
else if (response.data.role === "RestaurantOwner") {

    const restaurant =
        await restaurantService.getRestaurantByOwner(
            response.data.userId
        );

    localStorage.setItem(
        "restaurantId",
        restaurant.restaurantId
    );

    navigate("/owner/dashboard");
}
else {
    navigate("/home");
}
        }
        catch{
            toast.error("Invalid email or password");
        }

    };
    return (
    <div className="login-page">

        <div className="login-overlay"></div>

        <div className="login-container">

            <div className="login-left">

                <h1>Welcome Back 🍲</h1>

                <p>
                    Discover delicious meals from top restaurants
                    and get them delivered to your doorstep.
                </p>

            </div>

            <div className="login-card">

                <h3 className="text-center mb-4">
                    Login
                </h3>

                <form onSubmit={handleSubmit}>

                    <input
                        className="form-control mb-3"
                        placeholder="Email"
                        name="email"
                        onChange={handleChange}
                    />

                    <input
                        className="form-control mb-3"
                        type="password"
                        placeholder="Password"
                        name="password"
                        onChange={handleChange}
                    />

                    <button
                        type="submit"
                        className="btn-login"
                    >
                        Login
                    </button>

                    <div className="text-center mt-4">

                        <p>
                            New User?
                        </p>

                        <button
                            type="button"
                            className="btn-register"
                            onClick={() => navigate("/register")}
                        >
                            Create Account
                        </button>

                    </div>

                </form>

            </div>

        </div>

    </div>
);
}
export default Login;