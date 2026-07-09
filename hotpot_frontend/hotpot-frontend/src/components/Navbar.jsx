import { useContext } from "react";
import { Link,useNavigate } from "react-router-dom";
import AuthContext from "../context/AuthContext";
import { jwtDecode } from "jwt-decode";
import { Nav } from "react-bootstrap";

export function Navbar(){
    const navigate=useNavigate();
    const {setUser}=useContext(AuthContext);
    const token=localStorage.getItem("token");
    let role="";
    if(token){
        const decoded=jwtDecode(token);
        role=decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
    }
    const logout=()=>{
        localStorage.removeItem("token");
        setUser(null);
        navigate("/login");

    };
    return(
        <nav className="navbar navbar-expand-lg navbar-dark bg-dark">
            <div className="container">

               <Link
    className="navbar-brand fw-bold"
    to={token ? "/home" : "/"}
>
                    🍲 HotPot
                </Link>

                <button
                    className="navbar-toggler"
                    data-bs-toggle="collapse"
                    data-bs-target="#navbar">

                    <span className="navbar-toggler-icon"></span>

                </button>
              

                <div
                    className="collapse navbar-collapse"
                    id="navbar">

                    <ul className="navbar-nav me-auto">

                        <li className="nav-item">

                            <Link
                                className="nav-link"
                                to="/home">

                                Home

                            </Link>

                        </li>

                        <li className="nav-item">

                            <Link
                                className="nav-link"
                                to="/restaurants">

                                Restaurants

                            </Link>

                        </li>

                    </ul>

                    <ul className="navbar-nav">

                        {!token && (

                            <>
                                <li className="nav-item">

                                    <Link
                                        className="nav-link"
                                        to="/login">

                                        Login

                                    </Link>

                                </li>

                                <li className="nav-item">

                                    <Nav.Link as={Link} to="/restaurants"> Restaurants</Nav.Link>

                                </li>

                            </>

                        )}

                        {role === "Customer" && (

                            <>

                                <li className="nav-item">

                                    <Link
                                        className="nav-link"
                                        to="/cart">

                                        Cart

                                    </Link>

                                </li>

                                <li className="nav-item">

                                    <Link
                                        className="nav-link"
                                        to="/orders">

                                        Orders

                                    </Link>

                                </li>

                                <li className="nav-item">

                                    <Link
                                        className="nav-link"
                                        to="/profile">

                                        Profile

                                    </Link>

                                </li>

                            </>

                        )}

                        {role === "Admin" && (

                            <>

                                <li className="nav-item">

                                    <Link
                                        className="nav-link"
                                        to="/admin/dashboard">

                                        Dashboard

                                    </Link>

                                </li>

                                <li className="nav-item">

                                    <Link
                                        className="nav-link"
                                        to="/admin/categories">

                                        Categories

                                    </Link>

                                </li>

                            </>

                        )}

                        {role === "RestaurantOwner" && (

                            <>

                                <li className="nav-item">

                                    <Link
                                        className="nav-link"
                                        to="/owner/dashboard">

                                        Dashboard

                                    </Link>

                                </li>

                                <li className="nav-item">

                                    <Link
                                        className="nav-link"
                                        to="/owner/menuitems">

                                        Menu

                                    </Link>

                                </li>

                            </>

                        )}

                        {token && (

                            <li className="nav-item">

                                <button
                                    className="btn btn-danger ms-2"
                                    onClick={logout}>

                                    Logout

                                </button>

                            </li>

                        )}

                    </ul>

                </div>

            </div>
            </nav>
    )


}