import { Link } from "react-router-dom";


import "./LandingPage.css";

export default function LandingPage() {
  return (
    <>
      {/* HERO SECTION */}
      <section className="hero">
    

    <div className="hero-container">

        <div className="hero-left">

            <span className="hero-badge">
                🍽 Premium Food Delivery
            </span>

            <h1>
                Experience Fine Dining
                <br />
                At Your Doorstep
            </h1>

            <p>
                Discover top-rated restaurants, chef-crafted meals,
                and lightning-fast delivery across your city.
            </p>

            <div className="hero-buttons">
                <Link
                    to="/login"
                    className="btn-primary"
                >
                    Order Now
                </Link>

                <Link
                    to="/restaurants"
                    className="btn-secondary"
                >
                    Explore Restaurants
                </Link>
            </div>

        </div>

    </div>
</section>

      {/* FEATURES */}
      <section className="section">

    <div className="stats-grid">

        <div className="stat-card">
            <h2>500+</h2>
            <p>Restaurants</p>
        </div>

        <div className="stat-card">
            <h2>20K+</h2>
            <p>Orders Delivered</p>
        </div>

        <div className="stat-card">
            <h2>4.9★</h2>
            <p>Customer Rating</p>
        </div>

        <div className="stat-card">
            <h2>30 min</h2>
            <p>Average Delivery</p>
        </div>

    </div>

</section>

      {/* POPULAR RESTAURANTS */}
      <section className="section">
        <h2 className="section-title">Popular Restaurants</h2>

        <div className="restaurant-grid">

          <div className="restaurant-card">
            <img
              src="https://images.unsplash.com/photo-1513104890138-7c749659a591"
              alt="Pizza"
            />
            <h3>Pizza Hub</h3>
            <p>Italian • Fast Food</p>
          </div>

          <div className="restaurant-card">
            <img
              src="https://images.unsplash.com/photo-1568901346375-23c9450c58cd"
              alt="Burger"
            />
            <h3>Burger House</h3>
            <p>Burgers • Snacks</p>
          </div>

          <div className="restaurant-card">
            <img
              src="https://images.unsplash.com/photo-1555939594-58d7cb561ad1"
              alt="Restaurant"
            />
            <h3>Food Paradise</h3>
            <p>Multi Cuisine</p>
          </div>

        </div>
      </section>

      {/* TESTIMONIALS */}
      <section className="section testimonial-section">
        <h2 className="section-title">Customer Reviews</h2>

        <div className="testimonial-grid">

          <div className="testimonial-card">
            ⭐⭐⭐⭐⭐
            <p>
              Amazing food delivery experience.
              Super fast and reliable.
            </p>
          </div>

          <div className="testimonial-card">
            ⭐⭐⭐⭐⭐
            <p>
              Beautiful UI and excellent restaurants.
            </p>
          </div>

          <div className="testimonial-card">
            ⭐⭐⭐⭐⭐
            <p>
              Ordering food has never been easier.
            </p>
          </div>

        </div>
      </section>

      {/* FOOTER */}
      <footer className="footer">
        <h2>HotPot 🍲</h2>

        <p>
          Bringing your favourite meals to your doorstep.
        </p>

        <p>© 2026 HotPot Food Delivery</p>
      </footer>
    </>
  );
}