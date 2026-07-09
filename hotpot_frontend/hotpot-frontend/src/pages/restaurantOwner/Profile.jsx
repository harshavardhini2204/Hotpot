import { useEffect, useState } from "react";
import restaurantService from "../../services/restaurantService";
import { toast } from "react-toastify";

function Profile() {

    const restaurantId = Number(localStorage.getItem("restaurantId"));

    const [restaurant, setRestaurant] = useState({
        restaurantName: "",
        location: "",
        contactNumber: "",
        email: "",
        description: "",
        logoUrl: "",
        coverImageUrl: "",
        rating: 0,
        deliveryTime: "",
        costForOne: "",
        isActive: true
    });

    const [loading, setLoading] = useState(true);
    const [isEditing, setIsEditing] = useState(false);

    useEffect(() => {

        const fetchRestaurant = async () => {

            try {

                const data =
                    await restaurantService.getRestaurantById(
                        restaurantId
                    );

                setRestaurant(data);

            }

            catch (error) {

                console.error(error);

                toast.error(
                    "Unable to load restaurant profile"
                );

            }

            finally {

                setLoading(false);

            }

        };

        fetchRestaurant();

    }, [restaurantId]);

    const handleChange = (e) => {

        const { name, value, type, checked } = e.target;

        setRestaurant({
            ...restaurant,
            [name]:
                type === "checkbox"
                    ? checked
                    : value
        });

    };

    const handleSubmit = async (e) => {

        e.preventDefault();

        try {

            await restaurantService.updateRestaurant(
                restaurantId,
                restaurant
            );

            toast.success(
                "Profile Updated Successfully"
            );

            setIsEditing(false);

        }

        catch (error) {

            console.error(error);

            toast.error(
                "Unable to update profile"
            );

        }

    };

    if (loading)
        return (
            <h3 className="text-center mt-5">
                Loading...
            </h3>
        );

    return (

        <div className="restaurant-profile-page">

            {/* Banner */}

            <div
                className="profile-banner"
                style={{
                    backgroundImage: `url(${restaurant.coverImageUrl || restaurant.logoUrl})`
                }}
            >
                <div className="banner-overlay">

                    <img
                        src={restaurant.logoUrl}
                        alt="Restaurant Logo"
                        className="restaurant-logo"
                    />

                    <div>

                        <h1>
                            {restaurant.restaurantName}
                        </h1>

                        <p>
                            📍 {restaurant.location}
                        </p>

                    </div>

                </div>

            </div>

            <div className="container py-5">

                <div className="profile-card">

                    <div className="profile-header">

                        <h2>
                            Restaurant Profile
                        </h2>

                        <span
                            className={`status-badge ${
                                restaurant.isActive
                                    ? "active"
                                    : "inactive"
                            }`}
                        >
                            {restaurant.isActive
                                ? "Active"
                                : "Inactive"}
                        </span>

                    </div>

                    <form onSubmit={handleSubmit}>

                        <div className="row">

                            <div className="col-md-6 mb-3">

                                <label>
                                    Restaurant Name
                                </label>

                                <input
                                    className="form-control"
                                    name="restaurantName"
                                    value={restaurant.restaurantName}
                                    onChange={handleChange}
                                    disabled={!isEditing}
                                />

                            </div>

                            <div className="col-md-6 mb-3">

                                <label>
                                    Location
                                </label>

                                <input
                                    className="form-control"
                                    name="location"
                                    value={restaurant.location}
                                    onChange={handleChange}
                                    disabled={!isEditing}
                                />

                            </div>

                            <div className="col-md-6 mb-3">

                                <label>
                                    Contact Number
                                </label>

                                <input
                                    className="form-control"
                                    name="contactNumber"
                                    value={restaurant.contactNumber}
                                    onChange={handleChange}
                                    disabled={!isEditing}
                                />

                            </div>

                            <div className="col-md-6 mb-3">

                                <label>
                                    Email
                                </label>

                                <input
                                    className="form-control"
                                    value={restaurant.email}
                                    disabled
                                />

                            </div>

                            <div className="col-md-12 mb-3">

                                <label>
                                    Description
                                </label>

                                <textarea
                                    rows="4"
                                    className="form-control"
                                    name="description"
                                    value={restaurant.description}
                                    onChange={handleChange}
                                    disabled={!isEditing}
                                />

                            </div>

                            <div className="col-md-6 mb-3">

                                <label>
                                    Logo URL
                                </label>

                                <input
                                    className="form-control"
                                    name="logoUrl"
                                    value={restaurant.logoUrl}
                                    onChange={handleChange}
                                    disabled={!isEditing}
                                />

                            </div>

                            <div className="col-md-6 mb-3">

                                <label>
                                    Cover Image URL
                                </label>

                                <input
                                    className="form-control"
                                    name="coverImageUrl"
                                    value={restaurant.coverImageUrl || ""}
                                    onChange={handleChange}
                                    disabled={!isEditing}
                                />

                            </div>

                        </div>

                        <div className="profile-actions">

                            {!isEditing ? (

                                <button
                                    type="button"
                                    className="btn btn-warning"
                                    onClick={() =>
                                        setIsEditing(true)
                                    }
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
                                        onClick={() =>
                                            window.location.reload()
                                        }
                                    >
                                        Cancel
                                    </button>
                                </>

                            )}

                        </div>

                    </form>

                </div>

            </div>

        </div>

    );

}

export default Profile;