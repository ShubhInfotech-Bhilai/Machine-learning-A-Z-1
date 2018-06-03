# Importing the libraries
import pandas as pd # Import/manage dataset library

# Importing the dataset using pandas. In this case our dataset is a CVS file
dataset = pd.read_csv('Sprints.csv')

############## Read independant variables #################
# Now we are going to select all our independant variables. In this case we 
# have the sprint #, hours member 1, hours member 2 and number of processed
# story points

# iloc - The first parameter are the rows of the dataset, the second parameter
# are the columns. -1 means we don't take the last column, because the last
# column is our dependant variable.
X = dataset.iloc[:, 1:-1].values

############## Read dependant variables #################
# In this dataset our only dependant variable is the last column
# which contains the number of processed story points
Y = dataset.iloc[:, 4].values

############## Apply multiple linear regression to training data #################
# Import the linear regression model
from sklearn.linear_model import LinearRegression
regressor = LinearRegression()

# Apply the linear regression model on our train data set
regressor.fit(X, Y)

############## Predict result #################
regressor.predict([(2, 50, 50)])