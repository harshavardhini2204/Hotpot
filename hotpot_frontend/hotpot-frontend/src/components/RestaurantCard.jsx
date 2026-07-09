import { Link } from "react-router-dom";

function RestaurantCard({ restaurant }) {

    return (

        <div className="col-lg-4 col-md-6 mb-4">

            <div className="card restaurant-card border-0 shadow-sm">

                <img
                    src={restaurant.coverImageUrl || restaurant.logUrl}
                    alt={restaurant.restaurantName}
                    className="restaurant-image"
                />

                <div className="card-body">

                    <div className="d-flex justify-content-between">

                        <h5 className="fw-bold">
                            {restaurant.restaurantName}
                        </h5>

                        <span className="rating-badge">
                            ⭐ {restaurant.rating}
                        </span>

                    </div>

                    <p className="text-muted mb-2">

                        📍 {restaurant.location}

                    </p>

                    <div className="d-flex justify-content-between small text-muted mb-3">

                        <span>
                            🕒 {restaurant.deliveryTime} mins
                        </span>

                        <span>
                            ₹{restaurant.costForOne} for one
                        </span>

                    </div>

                    <p className="restaurant-description">

                        {restaurant.description}

                    </p>

                    <Link
                        to={`/restaurants/${restaurant.restaurantId}`}
                        className="btn btn-success w-100"
                    >
                        View Menu
                    </Link>

                </div>

            </div>

        </div>

    );
}

export default RestaurantCard;