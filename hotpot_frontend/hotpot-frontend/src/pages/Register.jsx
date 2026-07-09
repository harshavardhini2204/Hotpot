import { useState } from "react";
import { useNavigate } from "react-router-dom";
import authService from "../services/authService";
import { toast } from "react-toastify";

 function Register(){
    const navigate=useNavigate();
    const[user,setUser]=useState({
        fullName:"",
        gender:"",
        email:"",
        phoneNumber:"",
        address:"",
        password:""
    });
    const handleChange=(e)=>{
        setUser({
            ...user,
            [e.target.name]:e.target.value
        });
    };
    const register=async (e)=>{
        e.preventDefault();
        try{
            await authService.register(user);
            toast.success("Registration Successful");
            navigate("/login");
        }
        catch(err)
        {
            toast.error(err.response.data.message);
        }
    };
    return(
        <div className="container mt-5">

            <div className="row justify-content-center">

                <div className="col-md-6">

                    <div className="card shadow">

                        <div className="card-body">

                            <h3 className="text-center">

                                Register

                            </h3>

                            <form onSubmit={register}>

                                <input className="form-control mb-2"
                                    placeholder="Full Name"
                                    name="fullName"
                                    onChange={handleChange} />

                                <select className="form-select mb-2"
                                    name="gender"
                                    onChange={handleChange}>

                                    <option>Male</option>

                                    <option>Female</option>

                                </select>

                                <input className="form-control mb-2"
                                    placeholder="Email"
                                    name="email"
                                    onChange={handleChange} />

                                <input className="form-control mb-2"
                                    placeholder="Phone"
                                    name="phoneNumber"
                                    onChange={handleChange} />

                                <textarea className="form-control mb-2"
                                    placeholder="Address"
                                    name="address"
                                    onChange={handleChange} />

                                <input className="form-control mb-3"
                                    type="password"
                                    placeholder="Password"
                                    name="password"
                                    onChange={handleChange} />

                                <button
                                    className="btn btn-success w-100">

                                    Register

                                </button>

                            </form>

                        </div>

                    </div>

                </div>

            </div>

        </div>

    );
    
};
export default Register;