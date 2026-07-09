import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";

import restaurantService from "../services/restaurantService";
import menuService from "../services/menuService";

import MenuCard from "../components/MenuCard";

function RestaurantDetails() {

    const { id } = useParams();

    const [restaurant, setRestaurant] = useState(null);
    const [menuItems, setMenuItems] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {

        const loadData = async () => {

            try {

                const restaurantData =
                    await restaurantService.getRestaurantById(id);

                const menuData =
                    await menuService.getMenuItemsByRestaurant(id);

                setRestaurant(restaurantData);
                setMenuItems(menuData);

            }
            catch (error) {

                console.error(error);

            }
            finally {

                setLoading(false);

            }

        };

        loadData();

    }, [id]);

    if (loading)
        return <h3 className="text-center mt-5">Loading...</h3>;

    if (!restaurant)
        return <h3 className="text-center mt-5">Restaurant not found.</h3>;

    return (

        <div className="container mt-4">

            <div className="card shadow p-4 mb-4">

                <h2>{restaurant.restaurantName}</h2>

                <p>{restaurant.description}</p>

                <p>

                    <strong>Location:</strong> {restaurant.location}

                </p>

                <p>

                    <strong>Phone:</strong> {restaurant.contactNumber}

                </p>

            </div>

            <div className="page-header">
    <h1>Our Menu</h1>
    <p>Choose your favourite dishes</p>
</div>

            <div className="row">

                {

                    menuItems.map(item => (

                        <MenuCard
                            key={item.menuItemId}
                            menuItem={item}
                        />

                    ))

                }

            </div>

        </div>

    );

}

export default RestaurantDetails;