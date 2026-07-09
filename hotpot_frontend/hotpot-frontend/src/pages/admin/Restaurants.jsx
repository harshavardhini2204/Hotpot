import { useEffect, useState } from "react";
import restaurantService from "../../services/restaurantService";
import { toast } from "react-toastify";

function Restaurants() {

    const [restaurants, setRestaurants] = useState([]);
    const [search, setSearch] = useState("");

    useEffect(() => {

        const fetchRestaurants = async () => {

            try {

                const data = await restaurantService.getRestaurants();

                setRestaurants(data);

            }

            catch (error) {

                console.error(error);

                toast.error("Unable to load restaurants");

            }

        };

        fetchRestaurants();

    }, []);

    const handleDelete = async (id) => {

        if (!window.confirm("Delete this restaurant?"))
            return;

        try {

            await restaurantService.deleteRestaurant(id);

            toast.success("Restaurant Deleted");

            setRestaurants(restaurants.filter(r => r.restaurantId !== id));

        }

        catch (error) {

            console.error(error);

            toast.error("Delete Failed");

        }

    };

    const toggleStatus = async (restaurant) => {

        try {

            const updatedRestaurant = {

                ...restaurant,

                isActive: !restaurant.isActive

            };

            await restaurantService.updateRestaurant(
                restaurant.restaurantId,
                updatedRestaurant
            );

            toast.success("Status Updated");

            setRestaurants(restaurants.map(r =>
                r.restaurantId === restaurant.restaurantId
                    ? updatedRestaurant
                    : r
            ));

        }

        catch (error) {

            console.error(error);

            toast.error("Update Failed");

        }

    };

    const filteredRestaurants = restaurants.filter(r =>
        r.restaurantName.toLowerCase().includes(search.toLowerCase())
    );

    return (
    <div className="restaurant-admin-page">

        <div className="page-header">
            <h1>🍽 Restaurant Management</h1>
            <p>Manage all partner restaurants from one place</p>
        </div>

        <div className="admin-toolbar">

            <input
                className="search-box"
                placeholder="🔍 Search Restaurant..."
                value={search}
                onChange={(e) => setSearch(e.target.value)}
            />

            <div className="restaurant-count">
                {filteredRestaurants.length} Restaurants
            </div>

        </div>

        <div className="table-modern restaurant-table">

            <table className="table mb-0">

                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Restaurant</th>
                        <th>Location</th>
                        <th>Email</th>
                        <th>Status</th>
                        <th>Actions</th>
                    </tr>
                </thead>

                <tbody>

                    {filteredRestaurants.map((restaurant) => (

                        <tr key={restaurant.restaurantId}>

                            <td>
                                #{restaurant.restaurantId}
                            </td>

                            <td>
                                <div className="restaurant-info">

                                    <img
                                        src={
                                            restaurant.coverImageUrl ||
                                            "https://images.unsplash.com/photo-1517248135467-4c7edcad34c4"
                                        }
                                        alt=""
                                        className="restaurant-thumb"
                                    />

                                    <div>
                                        <strong>
                                            {restaurant.restaurantName}
                                        </strong>
                                    </div>

                                </div>
                            </td>

                            <td>{restaurant.location}</td>

                            <td>{restaurant.email}</td>

                            <td>
                                {restaurant.isActive ? (
                                    <span className="status-badge active">
                                        Active
                                    </span>
                                ) : (
                                    <span className="status-badge inactive">
                                        Inactive
                                    </span>
                                )}
                            </td>

                            <td>

                                <div className="action-buttons">

                                    <button
                                        className={
                                            restaurant.isActive
                                                ? "btn-warning btn-sm"
                                                : "btn-success btn-sm"
                                        }
                                        onClick={() =>
                                            toggleStatus(restaurant)
                                        }
                                    >
                                        {restaurant.isActive
                                            ? "Deactivate"
                                            : "Activate"}
                                    </button>

                                    <button
                                        className="btn-danger btn-sm"
                                        onClick={() =>
                                            handleDelete(
                                                restaurant.restaurantId
                                            )
                                        }
                                    >
                                        Delete
                                    </button>

                                </div>

                            </td>

                        </tr>

                    ))}

                </tbody>

            </table>

        </div>

    </div>
);

}

export default Restaurants;