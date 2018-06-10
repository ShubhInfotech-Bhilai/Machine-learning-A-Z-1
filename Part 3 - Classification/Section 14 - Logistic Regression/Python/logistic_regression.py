# Importing the libraries
import numpy as np # Math library
import matplotlib.pyplot as plt #Plotting library
import pandas as pd # Import/manage dataset library

# Importing the dataset using pandas. In this case our dataset is a CVS file
dataset = pd.read_csv('Social_Network_Ads.csv')

############## Read independant variables #################
# Now we are going to select all our independant variables. In this case this
# will be the country, age and salary. We will store them in the X variable.

# iloc - first parameter are the rows of the dataset, the second parameter
# are the columns. -1 means we don't take the last column, because the last
# column is our dependant variable.
X = dataset.iloc[:, [2, 3]].values

############## Read dependant variables #################
# In this dataset our only dependant variable is the last column (index 3)
# which contains whether or not the customer purchased the item.  
Y = dataset.iloc[:, 4].values

############## Splitting the dataset into a training and a test set #################
from sklearn.cross_validation import train_test_split

# We are going to use 20% of our dataset as a test set. The training set will
# be used by our model to train. When it's done, it will use the test set to
# verify if it understands the correlations between the model
X_train, X_test, Y_train, Y_test = train_test_split(X, Y, test_size = 0.20, random_state = 0)

############## Feature scaling #################
# Variables need to be on the same scale. Because the estimated column contains
# much higher number then the age column, our machine learning algorithm will
# neglect the age column. Therefor, we need to put them on the same scale.
# For most libraries this won't be necessery because they do it themselves
from sklearn.preprocessing import StandardScaler
standardscaler_X = StandardScaler()
X_train = standardscaler_X.fit_transform(X_train)
X_test = standardscaler_X.transform(X_test)

############## Apply logistic regression #################
from sklearn.linear_model import LogisticRegression
logistic_regression = LogisticRegression(random_state = 0)
logistic_regression.fit(X_train, Y_train)

Y_pred = logistic_regression.predict(X_test)

############## Create confusion matrix #################
from sklearn.metrics import confusion_matrix
cm = confusion_matrix(Y_test, Y_pred)

############## Visualize the training set results #################