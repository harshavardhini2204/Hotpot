import { useEffect, useState } from "react";
import restaurantService from "../services/restaurantService";
import RestaurantCard from "../components/RestaurantCard";

function Restaurants() {

    const [restaurants, setRestaurants] = useState([]);
    const [keyword, setKeyword] = useState("");
    const [loading, setLoading] = useState(true);

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
            finally {

                setLoading(false);

            }

        };

        fetchRestaurants();

    }, []);

    const filteredRestaurants = restaurants.filter(r =>
        r.restaurantName
            .toLowerCase()
            .includes(keyword.toLowerCase())
    );

    return (

        <div className="container mt-5">

            <h2 className="text-center mb-4">
                Restaurants
            </h2>

            <div className="row mb-4">

                <div className="col-md-12">

                    <input
                        type="text"
                        className="form-control"
                        placeholder="Search restaurant..."
                        value={keyword}
                        onChange={(e) =>
                            setKeyword(e.target.value)
                        }
                    />

                </div>

            </div>

            {
                loading ?

                    <h4 className="text-center">
                        Loading Restaurants...
                    </h4>

                    :

                    <div className="row">

                        {
                            filteredRestaurants.length === 0 ?

                                <h5 className="text-center">
                                    No Restaurants Found
                                </h5>

                                :

                                filteredRestaurants.map(restaurant => (

                                    <RestaurantCard
                                        key={restaurant.restaurantId}
                                        restaurant={restaurant}
                                    />

                                ))
                        }

                    </div>
            }

        </div>

    );

}

export default Restaurants;