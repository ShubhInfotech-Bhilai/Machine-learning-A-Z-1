# Importing the libraries
import numpy as np # Math library
import matplotlib.pyplot as plt #Plotting library
import pandas as pd # Import/manage dataset library

# Importing the dataset using pandas. In this case our dataset is a CVS file
dataset = pd.read_csv('Salary_Data.csv')

############## Read independant variables #################
# Now we are going to select all our independant variables. In this case we only
# have one independent variable: The years of experience

# iloc - first parameter are the rows of the dataset, the second parameter
# are the columns. -1 means we don't take the last column, because the last
# column is our dependant variable.
X = dataset.iloc[:, :-1].values

############## Read dependant variables #################
# In this dataset our only dependant variable is the second column
# which contains the salary
Y = dataset.iloc[:, 1].values

############## Splitting the dataset into a training and a test set #################
from sklearn.cross_validation import train_test_split

# We are going to use 20% of our dataset as a test set. The training set will
# be used by our model to train. When it's done, it will use the test set to
# verify if it understands the correlations between the model
X_train, X_test, Y_train, Y_test = train_test_split(X, Y, test_size = 1/3, random_state = 0)

############## Fit the linear regession model to the training set #################
# Import the linear regression model
from sklearn.linear_model import LinearRegression
regressor = LinearRegression()

# Apply the linear regession model on our train data set
regressor.fit(X_train, Y_train)

############## Predict the test set results #################
Y_pred = regressor.predict(X_test)

############## Visualize the training set results #################
plt.scatter(X_train, Y_train, color = 'red')
plt.plot(X_train, regressor.predict(X_train), color = 'blue')
plt.title('Salary vs Experience (Training set)')
plt.xlabel('Experience')
plt.ylabel('Salary')
plt.show()

############## Visualize the test set results #################
plt.scatter(X_test, Y_test, color = 'red')
plt.plot(X_train, regressor.predict(X_train), color = 'blue')
plt.title('Salary vs Experience (Test set)')
plt.xlabel('Experience')
plt.ylabel('Salary')
plt.show()