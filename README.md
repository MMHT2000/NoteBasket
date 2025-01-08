# NoteBasket

Project Overview: NoteBasket is a premium note-sharing platform designed to offer students access to high-quality study materials through a subscription-based model. Users can choose from three subscription levels: Free, Silver, and Gold. Only Admins and NoteMasters are authorized to upload notes, while users can access them based on their subscription.
Key Features:
•	General User Types:
o	Free: View-only access to free notes.
o	Silver: Access to free and silver-tier notes, ability to download.
o	Gold: Full access to all notes, including exclusive gold-tier content.
•	Admin & NoteMaster Roles:
o	Admins: Manage users, subscription levels, and approve/reject notes.
o	NoteMasters: Upload notes and categorize them.
	Anyone Can be NoteMasters, But it is for admin to decide wether or not to accept the note.
•	Payment Simulation:
o	A mock payment gateway simulates the subscription upgrade process.
o	Users can upgrade to Silver or Gold by simulating a payment.
•	Features to Implement:
o	Note Rating and Review System: Allow users to rate and review notes. Store ratings and reviews in the database and display average ratings.
o	Notes Bookmark: Students Can Bookmark Notes that they would like to see later.
o	Loyalty Points: Gives User Points for using the app which can be redeemed later for upgrading.
o	Search and Filter Functionality: Enable users to search for notes by keywords, categories, and tags. Add filtering options for category, rating, etc.
o	Subscription Expiration Notifications: Notify users when their subscription is about to expire. Send on-screen notifications.
o	Download History and Activity Tracking: Track user activity (downloads, views, ratings) and display it on the user’s profile.
o	Admin Dashboard for Analytics: Provide admins with insights such as the number of users, popular notes, and active subscriptions.
o	Notes Categorization and Tagging: Allow NoteMasters to tag and categorize notes when uploading. Enable users to filter notes by tags or categories.
o	Payment Simulation for Subscription Upgrades: Simulate a mock payment gateway for upgrading subscriptions (Silver/Gold).
Database Structure:
•	Users: Stores user information, including subscription level.
•	Notes: Stores uploaded notes by Admins and NoteMasters.
•	Subscription: Tracks user subscriptions and expiration.
•	Ratings: Stores user ratings and reviews for notes.
•	UserActivity: Tracks user activities (downloads, views, ratings).
•	Tags: Stores tags for categorizing notes.
•	NoteTags: Links notes to their respective tags.
Technology Stack:
•	Front-End: Windows Forms for UI.
•	Back-End: C# for database interaction and logic.
•	Database: MS SQL for user and note management.

