# Importing the libraries
import numpy as np # Math library
import matplotlib.pyplot as plt #Plotting library
import pandas as pd # Import/manage dataset library

# Importing the dataset using pandas. In this case our dataset is a CVS file
dataset = pd.read_csv('50_Startups.csv')

############## Read independant variables #################
# Now we are going to select all our independant variables. In this case we only
# have one independent variable: The years of experience

# iloc - The first parameter are the rows of the dataset, the second parameter
# are the columns. -1 means we don't take the last column, because the last
# column is our dependant variable.
X = dataset.iloc[:, :-1].values

############## Read dependant variables #################
# In this dataset our only dependant variable is the last column
# which contains the profit
Y = dataset.iloc[:, 4].values

############## Taking care of the categorical data #################
# In this case the state is categorical data, so we need to convert it to a 
# numerical value
from sklearn.preprocessing import LabelEncoder
labelencoder_X = LabelEncoder()
X[:, 3] = labelencoder_X.fit_transform(X[:, 3])

# Now we need to use onehotencoder to transform the state column into multiple
# columns
from sklearn.preprocessing import OneHotEncoder
onehotencoder = OneHotEncoder(categorical_features = [3])
X = onehotencoder.fit_transform(X).toarray()

############## Avoiding the dummy variable trap #################
# Remove the first dummy variable column from the data set
X = X[:, 1:]

############## Splitting the dataset into a training and a test set #################
from sklearn.cross_validation import train_test_split

# We are going to use 20% of our dataset as a test set. The training set will
# be used by our model to train. When it's done, it will use the test set to
# verify if it understands the correlations between the model
X_train, X_test, Y_train, Y_test = train_test_split(X, Y, test_size = 0.2, random_state = 0)

############## Apply multiple linear regression to training data #################
# Import the linear regression model
from sklearn.linear_model import LinearRegression
regressor = LinearRegression()

# Apply the linear regession model on our train data set
regressor.fit(X_train, Y_train)

############## Predict the test set results #################
Y_pred = regressor.predict(X_test)