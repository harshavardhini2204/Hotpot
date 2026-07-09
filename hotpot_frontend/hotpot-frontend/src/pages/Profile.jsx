import { useEffect, useState } from "react";
import { toast } from "react-toastify";
import userService from "../services/userService";

function Profile() {

    const userId = Number(localStorage.getItem("userId"));

    const [user, setUser] = useState({

        fullName: "",
        email: "",
        phoneNumber: "",
        address: "",
        role: ""

    });

    const [loading, setLoading] = useState(true);
    const [isEditing, setIsEditing] = useState(false);

    useEffect(() => {
         console.log("PROFILE MOUNTED");

        const loadProfile = async () => {

            try {

                const data = await userService.getProfile(userId);

                setUser(data);

            }

            catch (error) {

                console.error(error);

                toast.error("Unable to load profile");

            }

            finally {

                setLoading(false);

            }

        };

        loadProfile();

    }, [userId]);

    const handleChange = (e) => {

        const { name, value } = e.target;

        setUser({

            ...user,

            [name]: value

        });

    };

    const handleSubmit = async (e) => {

        e.preventDefault();

        try {

            const updatedUser = {

                fullName: user.fullName,

                phoneNumber: user.phoneNumber,

                address: user.address

            };

            await userService.updateProfile(userId, updatedUser);

            toast.success("Profile Updated Successfully");

setIsEditing(false);

        }

        catch (error) {

            console.error(error);

            toast.error("Failed to update profile");

        }

    };

    if (loading)

        return (

            <div className="container mt-5 text-center">

                <div className="spinner-border text-primary"></div>

            </div>

        );
       

    return (

        <div className="container mt-5">

            <div className="card shadow p-4">

                <h2 className="mb-4">

                    My Profile

                </h2>

                <form onSubmit={handleSubmit}>

                    <div className="mb-3">

                        <label className="form-label">

                            Full Name

                        </label>

                       <input
    type="text"
    className="form-control"
    name="fullName"
    value={user.fullName}
    onChange={handleChange}
    disabled={!isEditing}
/>

                    </div>

                    <div className="mb-3">

                        <label className="form-label">

                            Email

                        </label>

                        <input
                            type="email"
                            className="form-control"
                            value={user.email}
                            disabled
                        />

                        <small className="text-muted">

                            Email cannot be changed.

                        </small>

                    </div>

                    <div className="mb-3">

                        <label className="form-label">

                            Phone Number

                        </label>

                       <input
    type="text"
    className="form-control"
    name="phoneNumber"
    value={user.phoneNumber || ""}
    onChange={handleChange}
    disabled={!isEditing}
/>

                    </div>

                    <div className="mb-3">

                        <label className="form-label">

                            Address

                        </label>

                       <textarea
    className="form-control"
    rows="3"
    name="address"
    value={user.address || ""}
    onChange={handleChange}
    disabled={!isEditing}
/>

                    </div>

                    <div className="mb-4">

                        <label className="form-label">

                            Role

                        </label>

                        <input
                            type="text"
                            className="form-control"
                            value={user.role}
                            disabled
                        />

                    </div>

                    <div className="d-flex gap-3">

    {!isEditing ? (

        <button
    type="button"
    className="btn btn-warning"
    onClick={() => setIsEditing(true)}
>
    Edit Profile
</button>

    ) : (

        <>
            <button
                type="submit"
                className="btn btn-success"
            >
                Save Changes
            </button>

            <button
                type="button"
                className="btn btn-secondary"
                onClick={() => {

                    window.location.reload();

                }}
            >
                Cancel
            </button>
        </>

    )}

</div>

                </form>

            </div>

        </div>

    );

}

export default Profile;