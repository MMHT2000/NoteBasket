# NoteBasket

## Project Overview

**NoteBasket** is a premium note-sharing platform designed to offer students access to high-quality study materials through a subscription-based model. Users can choose from three subscription levels: **Free**, **Silver**, and **Gold**. Only Admins and NoteMasters are authorized to upload notes, while users can access them based on their subscription level.

---

## Key Features

### General User Types
- **Free**: View-only access to free notes.
- **Silver**: Access to free and silver-tier notes; ability to download.
- **Gold**: Full access to all notes, including exclusive gold-tier content.

### Admin & NoteMaster Roles
- **Admins**:
  - Manage users and subscription levels.
  - Approve or reject uploaded notes.
- **NoteMasters**:
  - Upload notes and categorize them.
  - Anyone can apply to become a NoteMaster, but admins decide whether to approve the application.

### Payment Simulation
- A mock payment gateway simulates the subscription upgrade process.
- Users can upgrade to Silver or Gold by simulating a payment.

### Features to Implement
1. **Note Rating and Review System**: Allow users to rate and review notes. Ratings and reviews will be stored in the database, with average ratings displayed.
2. **Notes Bookmarking**: Students can bookmark notes they wish to revisit later.
3. **Loyalty Points**: Award users points for app usage, which can later be redeemed for subscription upgrades.
4. **Search and Filter Functionality**: Enable users to:
   - Search for notes by keywords, categories, and tags.
   - Filter notes by category, rating, etc.
5. **Subscription Expiration Notifications**: Notify users when their subscription is about to expire via on-screen notifications.
6. **Download History and Activity Tracking**:
   - Track user activities such as downloads, views, and ratings.
   - Display these activities on the user’s profile.
7. **Admin Dashboard for Analytics**:
   - Provide admins with insights such as the number of users, popular notes, and active subscriptions.
8. **Notes Categorization and Tagging**:
   - Allow NoteMasters to tag and categorize notes when uploading.
   - Enable users to filter notes by tags or categories.
9. **Payment Simulation for Subscription Upgrades**:
   - Simulate a mock payment gateway for upgrading subscriptions to Silver/Gold tiers.

---

## Database Structure

### Tables
1. **Users**: Stores user information, including subscription level.
2. **Notes**: Stores uploaded notes by Admins and NoteMasters.
3. **Subscription**: Tracks user subscriptions and expiration dates.
4. **Ratings**: Stores user ratings and reviews for notes.
5. **UserActivity**: Tracks user activities (downloads, views, ratings).
6. **Tags**: Stores tags for categorizing notes.
7. **NoteTags**: Links notes to their respective tags.

---

## Technology Stack

- **Front-End**: Windows Forms for the User Interface.
- **Back-End**: C# for database interaction and application logic.
- **Database**: MS SQL for user and note management.

---

## Getting Started

### Prerequisites
- Microsoft Visual Studio (latest version recommended)
- .NET Framework
- Microsoft SQL Server

### Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/mmHT2000/NoteBasket.git
   ```
2. Open the project in Visual Studio.
3. Configure the database connection(Change the Database source).
4. Import the SQL file as Import Data Tier Application is MS SQL Server.
5. Build and run the application.

### Usage
1. Register as a user and log in.
2. Explore available notes based on your subscription level.
3. Upgrade your subscription using the mock payment simulation.
4. Admins and NoteMasters can upload, manage, and categorize notes.
5. Rate, review, and bookmark notes as needed.

---

## Contribution

We welcome contributions to enhance NoteBasket! If you'd like to contribute:
1. Fork the repository.
2. Create a new branch (`feature/your-feature`).
3. Commit your changes and push them.
4. Open a pull request.

---

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

## Contact

For questions or support, please contact [mohaiminul.acc@gmail.com](mailto:mohaiminul.acc@gmail.com).
