public interface ItemObserver
{
    public void checkItems(int changeAmount, string statName);
    public void addItem(int changeAmount, string statName);
    public void removeItem(int changeAmount, string statName);


}