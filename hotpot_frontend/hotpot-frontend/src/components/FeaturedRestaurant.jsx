import { useEffect, useState } from "react";
import restaurantService from "../services/restaurantService";
import RestaurantCard from "./RestaurantCard";

function FeaturedRestaurants({ searchTerm }) {

    const [restaurants, setRestaurants] = useState([]);

    useEffect(() => {

        const fetchRestaurants = async () => {

            try {

                const data =
                    await restaurantService.getRestaurants();

                setRestaurants(data);

            }
            catch (error) {

                console.error(error);

            }

        };

        fetchRestaurants();

    }, []);

    const filteredRestaurants = restaurants.filter(r =>
    (r.restaurantName || "")
        .toLowerCase()
        .includes((searchTerm || "").toLowerCase())
);

    return (

        <div className="container mt-5">

            <h2 className="text-center mb-4">

                Featured Restaurants

            </h2>

            <div className="row">

                {
                    filteredRestaurants.map(restaurant => (

                        <RestaurantCard
                            key={restaurant.restaurantId}
                            restaurant={restaurant}
                        />

                    ))
                }

            </div>

        </div>

    );

}

export default FeaturedRestaurants;